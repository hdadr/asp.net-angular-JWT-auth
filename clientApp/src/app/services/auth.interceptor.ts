import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from '../auth/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isRefreshingAccessToken(req.url)) { // refreshing accessToken
      return next.handle(req);
    }

    const accessToken = this.authService.getAccessToken();

    if (this.shouldRefreshAccessToken(accessToken)) { // accessToken expired but refreshToken is still valid
      return this.authService.refreshAccessToken().pipe(switchMap(response => {
        const request = this.setHeader(req, response.accessToken);
        return next.handle(request);
      }));
    }

    const request = this.setHeader(req, accessToken);
    return next.handle(request).pipe( // accessToken is valid or there is no accessToken (login)
      catchError((error: HttpErrorResponse) => { // access and refresh token expired
        if (this.unAuthorizedRequest(error)) {
          this.authService.logOut();
          return throwError(error);
        }
      })
    );
  }

  private setHeader(request: HttpRequest<any>, accessToken: string): HttpRequest<any> {
    return request.clone({
      headers: request.headers
        .set('Authorization', `Bearer ${accessToken}`)
        .set('Content-Type', 'application/json'),
    });
  }

  private shouldRefreshAccessToken(accessToken: string) {
    return !!accessToken && this.authService.isAccessTokenExpired() && !this.authService.isRefreshTokenExpired();
  }

  private isRefreshingAccessToken(url: string): boolean {
    return url.includes("token/refresh");
  }

  private unAuthorizedRequest(error: HttpErrorResponse) {
    return error.status === 401;
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateAuthTokensRequest } from '../../models/login.model';
import { User } from '../../models/user.model';
import { TokenService } from './token.service';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RefreshToken } from 'src/app/models/refresh-token.model';
import { ApiEndpoints } from 'src/app/config/api-endpoints';


@Injectable()
export class AuthService {
  private user: User;

  constructor(private http: HttpClient, private tokenService: TokenService, private apiEndpoints: ApiEndpoints) {
    const refreshToken = this.tokenService.getRefreshToken();
    if (refreshToken && this.isRefreshTokenExpired()) {
      this.logOut();
    }

    const accessToken = this.tokenService.getAccessToken()
    if (accessToken) {
      this.setUser(accessToken);
    }
  }

  public requestAuthTokens(request: CreateAuthTokensRequest): Observable<{ accessToken: string,  refreshToken: RefreshToken }> {
    return this.http.post<{ accessToken: string,  refreshToken: RefreshToken }>(this.apiEndpoints.API_TOKEN_CREATE, request).pipe(
        tap(response => {
          this.tokenService.saveAuthTokens(response.accessToken, response.refreshToken);
          this.setUser(response.accessToken);
      }));
  }

  public refreshAccessToken(): Observable<{ accessToken: string }> {
    const request = {
      accessToken: this.tokenService.getAccessToken(),
      refreshToken: this.tokenService.getRefreshToken()
    };

    return this.http.post<{ accessToken: string }>(this.apiEndpoints.API_TOKEN_REFRESH, request).pipe(
      tap(response => {
        this.tokenService.updateAccessToken(response.accessToken);
        this.setUser(response.accessToken);
    }));
  }

  public revokeRefreshToken(refreshTokenId: string): Observable<{code: string, description: string}> {
    return this.http.post<{code: string, description: string}>(this.apiEndpoints.API_TOKEN_REVOKE, JSON.stringify(refreshTokenId));
  }

  public logOut(): void {
    this.revokeRefreshToken(this.tokenService.getRefreshToken().id).subscribe(_ => {
      this.tokenService.removeAuthTokens();
      this.user = null;
    });
  }

  public getUser(): User {
    return this.user;
  }

  public isAccessTokenExpired(): boolean {
    const expiry = this.tokenService.getAccessTokenClaims(this.user.accessToken).exp;
    // 100ms deducted so there is a less chance for the accessToken to expire during the request travelling to the server
    return (expiry - 100) < (Date.now() / 1000);
  }

  public isRefreshTokenExpired(): boolean {
    const expiry = this.tokenService.getRefreshToken().expires;
    return expiry < Date.now();
  }

  public getAccessToken(): string {
    return this.user?.accessToken;
  }

  private setUser(accessToken: string): void {
    const claims = this.tokenService.getAccessTokenClaims(accessToken);
    this.user = {
      userName: claims['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      roles: [ ...claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']?.split(',')],
      accessToken,
    };
  }
}

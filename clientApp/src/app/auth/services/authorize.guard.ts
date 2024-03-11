import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from './auth.service';

@Injectable()
export class AuthorizeGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): boolean {
    const user = this.authService.getUser();
    if (!user) {
      this.router.navigate(['/login']);
      return false;
    }

    if (this.authService.isRefreshTokenExpired()) {
      this.authService.logOut();
      this.router.navigate(['/login', { session: 'expired' }]);
      return false;
    }

    const requiredRoleToActivate: string[] | null = route.data.canSee;
    if (!this.doesUserHasRightToActivate(user, requiredRoleToActivate)) {
      return false;
    }

    return true;
  }

  private doesUserHasRightToActivate(user: User, requiredRoleToActivate: string[]) {
    const isRoleRequiredToActivate = requiredRoleToActivate !== undefined;
    return isRoleRequiredToActivate ? this.doesUserHasRequiredRole(user, requiredRoleToActivate) : true;
  }

  private doesUserHasRequiredRole(user: User, requiredRoleToActivate: string[]) {
    return user.roles.map((role) => requiredRoleToActivate.includes(role)).includes(true);
  }
}

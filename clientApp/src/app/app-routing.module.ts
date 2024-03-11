import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthorizeGuard } from './auth/services/authorize.guard';
import { AdminRoleComponent } from './guarded-component/admin-role/admin-role.component';
import { GuardedComponentComponent } from './guarded-component/guarded-component.component';
import { LoggedInUserComponent } from './guarded-component/logged-in-user/logged-in-user.component';
import { UserAdminRoleComponent } from './guarded-component/user-admin-role/user-admin-role.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  {
    path: 'guarded-component',
    component: GuardedComponentComponent,
    canActivate: [AuthorizeGuard],
    children: [
      { path: 'logged-in-user', component: LoggedInUserComponent, canActivate: [AuthorizeGuard] },
      {
        path: 'user-or-admin-role',
        component: UserAdminRoleComponent,
        canActivate: [AuthorizeGuard],
        data: { canSee: ['User', 'Admin'] },
      },
      { path: 'admin-role', component: AdminRoleComponent, canActivate: [AuthorizeGuard], data: { canSee: ['Admin'] } },
    ],
  },
  { path: '',   redirectTo: 'login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}

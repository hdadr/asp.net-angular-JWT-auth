import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './auth/services/auth.service';
import { TokenService } from './auth/services/token.service';
import { LoginComponent } from './auth/login/login.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { GuardedComponentComponent } from './guarded-component/guarded-component.component';
import { AuthorizeGuard } from './auth/services/authorize.guard';
import { MatTabsModule } from '@angular/material/tabs';
import { LoggedInUserComponent } from './guarded-component/logged-in-user/logged-in-user.component';
import { UserAdminRoleComponent } from './guarded-component/user-admin-role/user-admin-role.component';
import { AdminRoleComponent } from './guarded-component/admin-role/admin-role.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { MatListModule } from '@angular/material/list';
import { TestService } from './services/test.service';
import { ApiEndpoints } from './config/api-endpoints';
import { UserService } from './services/user.service';

const materialModules = [
  MatInputModule, MatFormFieldModule, MatButtonModule, MatToolbarModule,
  MatMenuModule, MatTabsModule, MatListModule
];

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    GuardedComponentComponent,
    LoggedInUserComponent,
    UserAdminRoleComponent,
    AdminRoleComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,

    materialModules
  ],
  providers: [
    AuthService,
    TokenService,
    AuthorizeGuard,
    TestService,
    ApiEndpoints,
    UserService,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

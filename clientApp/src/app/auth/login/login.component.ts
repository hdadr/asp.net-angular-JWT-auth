import { HttpErrorResponse } from '@angular/common/http';
import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {
  public loginForm: FormGroup;
  public isLoading = false;
  public isSessionExpired: "expired" | string;

  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private cd: ChangeDetectorRef
  ) {
    this.isSessionExpired = this.route.snapshot.paramMap.get("session");
    const registeredUserUserName = this.router.getCurrentNavigation().extras.state?.userName;
    this.loginForm = this.fb.group({
      UserName: [registeredUserUserName ?? '', Validators.required],
      Password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  public login(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.isLoading = true;

    this.authService.requestAuthTokens(this.loginForm.value).subscribe(
      (_) => {
        this.isLoading = false;
        this.router.navigateByUrl('/guarded-component/logged-in-user');
        this.cd.markForCheck();
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse)
        this.isLoading = false;
        this.cd.markForCheck();
      }
    );
  }
}

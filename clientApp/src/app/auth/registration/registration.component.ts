import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { RegistrationRequest } from 'src/app/models/registration.model';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from '../services/auth.service';


const passwordsDoNotMatchValidator: ValidatorFn = (formGroup: FormGroup): ValidationErrors | null => {
  const password = formGroup.get('Password').value;
  const confirmPassword = formGroup.get('ConfirmPassword').value;

  return password !== confirmPassword ? { passwordsDoNotMatch: true } : null;
};


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidParent = !!(
      control
      && control.parent
      && control.parent.hasError('passwordsDoNotMatch')
    );
    return (invalidParent);
  }
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegistrationComponent implements OnInit {
  public registrationForm: FormGroup;
  public isLoading = false;
  errorStateMatcher = new MyErrorStateMatcher();

  constructor(private fb: FormBuilder, public userService: UserService, private router: Router, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      UserName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [ Validators.required, Validators.minLength(6)] ],
      ConfirmPassword: ['', Validators.required],
    }, { validators: passwordsDoNotMatchValidator });
  }

  public register(): void {
    if (this.registrationForm.invalid) {
      return;
    }

    this.isLoading = true;

    const formData = this.registrationForm.value as RegistrationRequest;
    this.userService.createUser(formData).subscribe(
      (_) => {
        this.isLoading = false;
        this.router.navigate(['/login'], { state: { userName: formData.UserName } });
        this.cd.markForCheck();
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        this.isLoading = false;
        this.cd.markForCheck();
      }
    );
  }

}

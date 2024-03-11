import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'clientApp';

  constructor(public authService: AuthService, private router: Router) {}

  public logOut(): void {
    this.authService.logOut();
    this.router.navigateByUrl('/login')
  }
}

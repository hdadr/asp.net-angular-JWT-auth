import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-logged-in-user',
  templateUrl: './logged-in-user.component.html',
  styleUrls: ['./logged-in-user.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoggedInUserComponent implements OnInit {
  datas$: Observable<{unauthorizedData: string[]}>;

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    this.datas$ = this.testService.getUnauthorizedDataLoggedInUser();
  }

}

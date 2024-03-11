import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-user-admin-role',
  templateUrl: './user-admin-role.component.html',
  styleUrls: ['./user-admin-role.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserAdminRoleComponent implements OnInit {
  datas$: Observable<{userRoleData: string[]}>;

  constructor(private testService: TestService) {}

  ngOnInit(): void {
    this.datas$ = this.testService.getDataForUserRole();
  }
}

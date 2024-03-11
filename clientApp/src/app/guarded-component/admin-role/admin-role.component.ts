import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Observable } from 'rxjs';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-admin-role',
  templateUrl: './admin-role.component.html',
  styleUrls: ['./admin-role.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminRoleComponent implements OnInit {
  datas$: Observable<{adminRoleData: string[]}>;

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    this.datas$ = this.testService.getDataForAdminRole();
  }

}

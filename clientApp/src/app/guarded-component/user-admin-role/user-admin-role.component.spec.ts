import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAdminRoleComponent } from './user-admin-role.component';

describe('UserAdminRoleComponent', () => {
  let component: UserAdminRoleComponent;
  let fixture: ComponentFixture<UserAdminRoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserAdminRoleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAdminRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

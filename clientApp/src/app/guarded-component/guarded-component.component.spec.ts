import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuardedComponentComponent } from './guarded-component.component';

describe('GuardedComponentComponent', () => {
  let component: GuardedComponentComponent;
  let fixture: ComponentFixture<GuardedComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuardedComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GuardedComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

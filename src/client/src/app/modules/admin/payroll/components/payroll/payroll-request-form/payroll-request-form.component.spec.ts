import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayrollRequestFormComponent } from './payroll-request-form.component';

describe('PayrollRequestFormComponent', () => {
  let component: PayrollRequestFormComponent;
  let fixture: ComponentFixture<PayrollRequestFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PayrollRequestFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PayrollRequestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

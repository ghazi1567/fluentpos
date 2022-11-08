import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayrollDetailViewComponent } from './payroll-detail-view.component';

describe('PayrollDetailViewComponent', () => {
  let component: PayrollDetailViewComponent;
  let fixture: ComponentFixture<PayrollDetailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PayrollDetailViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PayrollDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

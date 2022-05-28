import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarianceReportComponent } from './variance-report.component';

describe('VarianceReportComponent', () => {
  let component: VarianceReportComponent;
  let fixture: ComponentFixture<VarianceReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VarianceReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VarianceReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

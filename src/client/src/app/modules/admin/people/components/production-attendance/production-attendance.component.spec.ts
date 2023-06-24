import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionAttendanceComponent } from './production-attendance.component';

describe('ProductionAttendanceComponent', () => {
  let component: ProductionAttendanceComponent;
  let fixture: ComponentFixture<ProductionAttendanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductionAttendanceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductionAttendanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

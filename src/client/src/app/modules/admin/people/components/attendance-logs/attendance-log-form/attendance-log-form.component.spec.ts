import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceLogFormComponent } from './attendance-log-form.component';

describe('AttendanceLogFormComponent', () => {
  let component: AttendanceLogFormComponent;
  let fixture: ComponentFixture<AttendanceLogFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendanceLogFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendanceLogFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

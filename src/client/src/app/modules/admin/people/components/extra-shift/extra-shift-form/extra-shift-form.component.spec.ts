import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtraShiftFormComponent } from './extra-shift-form.component';

describe('ExtraShiftFormComponent', () => {
  let component: ExtraShiftFormComponent;
  let fixture: ComponentFixture<ExtraShiftFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExtraShiftFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExtraShiftFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

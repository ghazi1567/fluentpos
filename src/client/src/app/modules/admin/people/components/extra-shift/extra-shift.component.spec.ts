import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtraShiftComponent } from './extra-shift.component';

describe('ExtraShiftComponent', () => {
  let component: ExtraShiftComponent;
  let fixture: ComponentFixture<ExtraShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExtraShiftComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExtraShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

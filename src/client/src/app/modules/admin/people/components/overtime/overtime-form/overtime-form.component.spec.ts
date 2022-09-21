import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OvertimeFormComponent } from './overtime-form.component';

describe('OvertimeFormComponent', () => {
  let component: OvertimeFormComponent;
  let fixture: ComponentFixture<OvertimeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OvertimeFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OvertimeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OvetimePlanFormComponent } from './ovetime-plan-form.component';

describe('OvetimePlanFormComponent', () => {
  let component: OvetimePlanFormComponent;
  let fixture: ComponentFixture<OvetimePlanFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OvetimePlanFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OvetimePlanFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

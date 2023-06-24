import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverTimePlannerComponent } from './over-time-planner.component';

describe('OverTimePlannerComponent', () => {
  let component: OverTimePlannerComponent;
  let fixture: ComponentFixture<OverTimePlannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverTimePlannerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OverTimePlannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

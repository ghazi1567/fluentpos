import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignDepartmentComponent } from './assign-department.component';

describe('AssignDepartmentComponent', () => {
  let component: AssignDepartmentComponent;
  let fixture: ComponentFixture<AssignDepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignDepartmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

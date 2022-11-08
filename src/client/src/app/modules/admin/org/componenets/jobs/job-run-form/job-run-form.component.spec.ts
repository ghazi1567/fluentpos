import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobRunFormComponent } from './job-run-form.component';

describe('JobRunFormComponent', () => {
  let component: JobRunFormComponent;
  let fixture: ComponentFixture<JobRunFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobRunFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobRunFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationFormComponent } from './designation-form.component';

describe('DesignationFormComponent', () => {
  let component: DesignationFormComponent;
  let fixture: ComponentFixture<DesignationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignationFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignedToHeadofficeComponent } from './assigned-to-headoffice.component';

describe('AssignedToHeadofficeComponent', () => {
  let component: AssignedToHeadofficeComponent;
  let fixture: ComponentFixture<AssignedToHeadofficeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignedToHeadofficeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignedToHeadofficeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

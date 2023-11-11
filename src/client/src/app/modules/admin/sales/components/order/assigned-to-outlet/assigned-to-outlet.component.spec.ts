import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignedToOutletComponent } from './assigned-to-outlet.component';

describe('AssignedToOutletComponent', () => {
  let component: AssignedToOutletComponent;
  let fixture: ComponentFixture<AssignedToOutletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignedToOutletComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignedToOutletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncrementDecrementFormComponent } from './increment-decrement-form.component';

describe('IncrementDecrementFormComponent', () => {
  let component: IncrementDecrementFormComponent;
  let fixture: ComponentFixture<IncrementDecrementFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncrementDecrementFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncrementDecrementFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

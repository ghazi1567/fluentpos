import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SplitOrderComponent } from './split-order.component';

describe('SplitOrderComponent', () => {
  let component: SplitOrderComponent;
  let fixture: ComponentFixture<SplitOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SplitOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SplitOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

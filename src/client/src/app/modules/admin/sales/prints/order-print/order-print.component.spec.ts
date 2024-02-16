import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderPrintComponent } from './order-print.component';

describe('OrderPrintComponent', () => {
  let component: OrderPrintComponent;
  let fixture: ComponentFixture<OrderPrintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderPrintComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

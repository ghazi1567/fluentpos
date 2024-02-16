import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReturnOrderComponent } from './return-order.component';

describe('ReturnOrderComponent', () => {
  let component: ReturnOrderComponent;
  let fixture: ComponentFixture<ReturnOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReturnOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReturnOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockOutEditComponent } from './stock-out-edit.component';

describe('StockOutEditComponent', () => {
  let component: StockOutEditComponent;
  let fixture: ComponentFixture<StockOutEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockOutEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockOutEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockInEditComponent } from './stock-in-edit.component';

describe('StockInEditComponent', () => {
  let component: StockInEditComponent;
  let fixture: ComponentFixture<StockInEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockInEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockInEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

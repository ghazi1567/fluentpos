import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockInDetailComponent } from './stock-in-detail.component';

describe('StockInDetailComponent', () => {
  let component: StockInDetailComponent;
  let fixture: ComponentFixture<StockInDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockInDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockInDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

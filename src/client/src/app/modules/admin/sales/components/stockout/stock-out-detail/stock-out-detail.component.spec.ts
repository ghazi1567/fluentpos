import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockOutDetailComponent } from './stock-out-detail.component';

describe('StockOutDetailComponent', () => {
  let component: StockOutDetailComponent;
  let fixture: ComponentFixture<StockOutDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockOutDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockOutDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

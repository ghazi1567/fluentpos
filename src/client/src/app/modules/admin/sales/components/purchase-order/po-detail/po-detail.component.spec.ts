import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoDetailComponent } from './po-detail.component';

describe('PoDetailComponent', () => {
  let component: PoDetailComponent;
  let fixture: ComponentFixture<PoDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PoDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

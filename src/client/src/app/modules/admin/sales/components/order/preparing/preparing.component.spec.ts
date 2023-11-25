import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreparingComponent } from './preparing.component';

describe('PreparingComponent', () => {
  let component: PreparingComponent;
  let fixture: ComponentFixture<PreparingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreparingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreparingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

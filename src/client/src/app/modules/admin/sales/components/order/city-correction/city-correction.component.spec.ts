import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCorrectionComponent } from './city-correction.component';

describe('CityCorrectionComponent', () => {
  let component: CityCorrectionComponent;
  let fixture: ComponentFixture<CityCorrectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CityCorrectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityCorrectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

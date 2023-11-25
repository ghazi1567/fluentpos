import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateLoadSheetComponent } from './generate-load-sheet.component';

describe('GenerateLoadSheetComponent', () => {
  let component: GenerateLoadSheetComponent;
  let fixture: ComponentFixture<GenerateLoadSheetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenerateLoadSheetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenerateLoadSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

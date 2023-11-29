import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadSheetsComponent } from './load-sheets.component';

describe('LoadSheetsComponent', () => {
  let component: LoadSheetsComponent;
  let fixture: ComponentFixture<LoadSheetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadSheetsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

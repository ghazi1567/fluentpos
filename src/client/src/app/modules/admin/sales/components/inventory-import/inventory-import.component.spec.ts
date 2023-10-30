import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryImportComponent } from './inventory-import.component';

describe('InventoryImportComponent', () => {
  let component: InventoryImportComponent;
  let fixture: ComponentFixture<InventoryImportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InventoryImportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

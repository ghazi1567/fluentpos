import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryImportListComponent } from './inventory-import-list.component';

describe('InventoryImportListComponent', () => {
  let component: InventoryImportListComponent;
  let fixture: ComponentFixture<InventoryImportListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InventoryImportListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryImportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

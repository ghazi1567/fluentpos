import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadInvoiceComponent } from './upload-invoice.component';

describe('UploadInvoiceComponent', () => {
  let component: UploadInvoiceComponent;
  let fixture: ComponentFixture<UploadInvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadInvoiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

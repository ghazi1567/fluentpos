import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxAdminLayoutComponent } from './ngx-admin-layout.component';

describe('NgxAdminLayoutComponent', () => {
  let component: NgxAdminLayoutComponent;
  let fixture: ComponentFixture<NgxAdminLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NgxAdminLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NgxAdminLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

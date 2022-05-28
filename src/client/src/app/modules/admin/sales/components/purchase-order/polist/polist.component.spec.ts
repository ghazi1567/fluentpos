import { ComponentFixture, TestBed } from '@angular/core/testing';

import { POListComponent } from './polist.component';

describe('POListComponent', () => {
  let component: POListComponent;
  let fixture: ComponentFixture<POListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ POListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(POListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

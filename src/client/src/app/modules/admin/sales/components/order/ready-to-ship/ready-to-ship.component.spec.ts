import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadyToShipComponent } from './ready-to-ship.component';

describe('ReadyToShipComponent', () => {
  let component: ReadyToShipComponent;
  let fixture: ComponentFixture<ReadyToShipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReadyToShipComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReadyToShipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalPerksComponent } from './global-perks.component';

describe('GlobalPerksComponent', () => {
  let component: GlobalPerksComponent;
  let fixture: ComponentFixture<GlobalPerksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalPerksComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GlobalPerksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalPerkFormComponent } from './global-perk-form.component';

describe('GlobalPerkFormComponent', () => {
  let component: GlobalPerkFormComponent;
  let fixture: ComponentFixture<GlobalPerkFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GlobalPerkFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GlobalPerkFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

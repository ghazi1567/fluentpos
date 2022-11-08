import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CronGenComponent } from './cron-gen.component';

describe('CronGenComponent', () => {
  let component: CronGenComponent;
  let fixture: ComponentFixture<CronGenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CronGenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CronGenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

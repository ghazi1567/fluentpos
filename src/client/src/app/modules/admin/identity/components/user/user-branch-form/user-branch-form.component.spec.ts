import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBranchFormComponent } from './user-branch-form.component';

describe('UserBranchFormComponent', () => {
  let component: UserBranchFormComponent;
  let fixture: ComponentFixture<UserBranchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserBranchFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserBranchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrgRoutingModule } from './org-routing.module';
import { SetupComponent } from './componenets/setup/setup.component';
import { OrgComponent } from './componenets/org/org.component';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from 'src/app/core/material/material.module';
import { BranchComponent } from './componenets/branch/branch.component';
import { branchFormComponent } from './componenets/branch/branch-form/branch-form.component';
import { DepartmentComponent } from './componenets/department/department.component';
import { DepartmentFormComponent } from './componenets/department/department-form/department-form.component';
import { DesignationComponent } from './componenets/designation/designation.component';
import { DesignationFormComponent } from './componenets/designation/designation-form/designation-form.component';
import { PolicyComponent } from './componenets/policy/policy.component';
import { PolicyFormComponent } from './componenets/policy/policy-form/policy-form.component';
import { JobsComponent } from './componenets/jobs/jobs.component';
import { JobFormComponent } from './componenets/jobs/job-form/job-form.component';
import { JobRunFormComponent } from './componenets/jobs/job-run-form/job-run-form.component';


@NgModule({
  declarations: [
    SetupComponent,
    OrgComponent,
    BranchComponent,
    branchFormComponent,
    DepartmentComponent,
    DepartmentFormComponent,
    DesignationComponent,
    DesignationFormComponent,
    PolicyComponent,
    PolicyFormComponent,
    JobsComponent,
    JobFormComponent,
    JobRunFormComponent,
  ],
  imports: [
    CommonModule,
    OrgRoutingModule,
    MaterialModule,
    SharedModule,
    TranslateModule,
  ]
})
export class OrgModule { }

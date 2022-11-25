import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PayrollRoutingModule } from './payroll-routing.module';
import { EmployeeSalaryComponent } from './components/employee-salary/employee-salary.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { IncrementDecrementFormComponent } from './components/employee-salary/increment-decrement-form/increment-decrement-form.component';
import { PayrollComponent } from './components/payroll/payroll.component';
import { PayrollRequestFormComponent } from './components/payroll/payroll-request-form/payroll-request-form.component';
import { PayrollDetailViewComponent } from './components/payroll/payroll-detail-view/payroll-detail-view.component';
import { PerksComponent } from './components/employee-salary/perks/perks.component';


@NgModule({
  declarations: [
    EmployeeSalaryComponent,
    IncrementDecrementFormComponent,
    PayrollComponent,
    PayrollRequestFormComponent,
    PayrollDetailViewComponent,
    PerksComponent
  ],
  imports: [
    CommonModule,
    PayrollRoutingModule,
    MaterialModule,
    SharedModule,
    TranslateModule,
    
  ]
})
export class PayrollModule { }

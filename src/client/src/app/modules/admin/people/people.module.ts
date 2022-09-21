import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PeopleRoutingModule } from './people-routing.module';
import { CustomerComponent } from './components/customer/customer.component';
import { SupplierComponent } from './components/supplier/supplier.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { CustomerService } from './services/customer.service';
import { CustomerFormComponent } from './components/customer/customer-form/customer-form.component';
import { PeopleComponent } from './people.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { EmployeeFormComponent } from './components/employee/employee-form/employee-form.component';
import { AttendanceComponent } from './components/attendance/attendance.component';
import { AttendanceFormComponent } from './components/attendance/attendance-form/attendance-form.component';
import { MyQueueComponent } from './components/my-queue/my-queue.component';
import { OvertimeComponent } from './components/overtime/overtime.component';
import { OvertimeFormComponent } from './components/overtime/overtime-form/overtime-form.component';


@NgModule({
  declarations: [
    CustomerComponent,
    SupplierComponent,
    CustomerFormComponent,
    PeopleComponent,
    EmployeeComponent,
    EmployeeFormComponent,
    AttendanceComponent,
    AttendanceFormComponent,
    MyQueueComponent,
    OvertimeComponent,
    OvertimeFormComponent
  ],
  imports: [
    CommonModule,
    PeopleRoutingModule,
    MaterialModule,
    SharedModule
  ],
  providers: [
    CustomerService,
  ]
})
export class PeopleModule { }

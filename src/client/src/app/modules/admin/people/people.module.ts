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
import { AttendanceLogsComponent } from './components/attendance-logs/attendance-logs.component';
import { AttendanceLogFormComponent } from './components/attendance-logs/attendance-log-form/attendance-log-form.component';
import { IndividualReportComponent } from './components/attendance/individual-report/individual-report.component';
import { AttendanceReportComponent } from './components/attendance/attendance-report/attendance-report.component';
import { EmpListComponent } from './components/employee/emp-list/emp-list.component';
import { AssignDepartmentComponent } from './components/employee/assign-department/assign-department.component';
import { ExtraShiftComponent } from './components/extra-shift/extra-shift.component';
import { ExtraShiftFormComponent } from './components/extra-shift/extra-shift-form/extra-shift-form.component';
import { OverTimePlannerComponent } from './components/over-time-planner/over-time-planner.component';
import { ProductionAttendanceComponent } from './components/production-attendance/production-attendance.component';
import { OvetimePlanFormComponent } from './components/over-time-planner/ovetime-plan-form/ovetime-plan-form.component';


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
    OvertimeFormComponent,
    AttendanceLogsComponent,
    AttendanceLogFormComponent,
    IndividualReportComponent,
    AttendanceReportComponent,
    EmpListComponent,
    AssignDepartmentComponent,
    ExtraShiftComponent,
    ExtraShiftFormComponent,
    OverTimePlannerComponent,
    ProductionAttendanceComponent,
    OvetimePlanFormComponent
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

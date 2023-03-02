import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AttendanceLogsComponent } from "./components/attendance-logs/attendance-logs.component";
import { AttendanceComponent } from "./components/attendance/attendance.component";
import { CustomerComponent } from "./components/customer/customer.component";
import { EmpListComponent } from "./components/employee/emp-list/emp-list.component";
import { EmployeeComponent } from "./components/employee/employee.component";
import { MyQueueComponent } from "./components/my-queue/my-queue.component";
import { OvertimeComponent } from "./components/overtime/overtime.component";

const routes: Routes = [
    {
        path: "customers",
        component: CustomerComponent
    },
    {
        path: "employees",
        component: EmpListComponent
    },
    {
        path: "attendances",
        component: AttendanceComponent
    },
    {
        path: "attendances-logs",
        component: AttendanceLogsComponent
    },
    {
        path: "my-queue",
        component: MyQueueComponent
    },
    {
        path: "overtimes",
        component: OvertimeComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PeopleRoutingModule {}

import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { EmployeeSalaryComponent } from "./components/employee-salary/employee-salary.component";
import { PayrollDetailViewComponent } from "./components/payroll/payroll-detail-view/payroll-detail-view.component";
import { PayrollComponent } from "./components/payroll/payroll.component";

const routes: Routes = [
    {
        path: "employee-salaries",
        component: EmployeeSalaryComponent
    },
    {
        path: "payroll",
        component: PayrollComponent
    },
    {
        path: "payslip/:id",
        component: PayrollDetailViewComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PayrollRoutingModule {}

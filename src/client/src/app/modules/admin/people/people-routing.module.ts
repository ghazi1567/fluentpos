import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CustomerComponent } from "./components/customer/customer.component";
import { EmployeeComponent } from "./components/employee/employee.component";

const routes: Routes = [
    {
        path: "customers",
        component: CustomerComponent
    },
    {
        path: "employees",
        component: EmployeeComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PeopleRoutingModule {}

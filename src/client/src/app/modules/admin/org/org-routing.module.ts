import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { BranchComponent } from "./componenets/branch/branch.component";
import { DepartmentComponent } from "./componenets/department/department.component";
import { DesignationComponent } from "./componenets/designation/designation.component";
import { JobsComponent } from "./componenets/jobs/jobs.component";
import { PolicyComponent } from "./componenets/policy/policy.component";
import { SetupComponent } from "./componenets/setup/setup.component";

const routes: Routes = [
    {
        path: "setup",
        component: SetupComponent
    },
    {
        path: "branch",
        component: BranchComponent
    },
    {
        path: "department",
        component: DepartmentComponent
    },
    {
        path: "designation",
        component: DesignationComponent
    },
    {
        path: "policies",
        component: PolicyComponent
    },
    {
        path: "jobs",
        component: JobsComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrgRoutingModule {}

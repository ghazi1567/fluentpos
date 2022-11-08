import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { OverTime, OverTimeMapping } from "src/app/core/enums/OverTime";
import { PayslipTypeMapping } from "src/app/core/enums/PayslipType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Department } from "../../models/Department";
import { Policy } from "../../models/policy";
import { SearchParams } from "../../models/SearchParams";
import { DepartmentService } from "../../services/department.service";
import { PolicyService } from "../../services/policy.service";
import { PolicyFormComponent } from "./policy-form/policy-form.component";

@Component({
    selector: "app-policy",
    templateUrl: "./policy.component.html",
    styleUrls: ["./policy.component.scss"]
})
export class PolicyComponent implements OnInit {
    policies: PaginatedResult<Policy>;
    departments: PaginatedResult<Department>;
    policiesColumns: TableColumn[];
    designationParams = new SearchParams();
    searchString: string;
    public PayslipTypeMapping = PayslipTypeMapping;
    public OverTimeMapping = OverTimeMapping;

    constructor(public departmentService: DepartmentService, public policyService: PolicyService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getPolicys();
        this.initColumns();
    }

    getPolicys(): void {
        this.departmentService.getAlls(this.designationParams).subscribe((result) => {
            this.departments = result;
            this.getPolicies();
        });
    }
    getPolicyName(departmentId: any) {
        if (departmentId) {
            var department = this.departments.data.find((x) => x.id == departmentId);
            if (department) {
                return department.name;
            }
        }
        return "";
    }

    getPolicies(): void {
        this.policyService.getAlls(this.designationParams).subscribe((result) => {
            result.data.forEach((x) => {
                x.departmentName = this.getPolicyName(x.departmentId);
                x.timings = `${x.shiftStartTime} - ${x.shiftEndTime}`;
                x.payslipTypeName = PayslipTypeMapping[x.payslipType];
                x.overTimeTypeName = x.dailyOverTime == OverTime.UnPaid ? "Un Paid" : "Paid";
            });
            this.policies = result;
        });
    }
    initColumns(): void {
        this.policiesColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Policy Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "Department", dataKey: "departmentName", isSortable: true, isShowable: true },
            { name: "Timings", dataKey: "timings", isSortable: true, isShowable: true },
            { name: "Payslip Type", dataKey: "payslipTypeName", isSortable: true, isShowable: true },
            { name: "Over Time", dataKey: "overTimeTypeName", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.designationParams.pageNumber = event.pageNumber;
        this.designationParams.pageSize = event.pageSize;
        this.getPolicies();
    }

    openForm(Branch?: Policy): void {
        const dialogRef = this.dialog.open(PolicyFormComponent, {
            data: Branch
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getPolicies();
        });
    }

    remove($event: string): void {
        this.policyService.delete($event).subscribe(() => {
            this.getPolicies();
            this.toastr.info("Policy Removed");
        });
    }

    sort($event: Sort): void {
        this.designationParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.designationParams.orderBy);
        this.getPolicies();
    }

    filter($event: string): void {
        this.designationParams.searchString = $event.trim().toLocaleLowerCase();
        this.designationParams.pageNumber = 0;
        this.designationParams.pageSize = 0;
        this.getPolicies();
    }

    reload(): void {
        this.designationParams.searchString = "";
        this.designationParams.pageNumber = 0;
        this.designationParams.pageSize = 0;
        this.getPolicies();
    }
}

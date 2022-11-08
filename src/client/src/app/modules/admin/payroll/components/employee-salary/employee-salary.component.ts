import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { SalaryPerksType, SalaryPerksTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Salary } from "../../models/salary";
import { SearchParams } from "../../models/SearchParams";
import { SalaryService } from "../../services/salary.service";
import { IncrementDecrementFormComponent } from "./increment-decrement-form/increment-decrement-form.component";

@Component({
    selector: "app-employee-salary",
    templateUrl: "./employee-salary.component.html",
    styleUrls: ["./employee-salary.component.scss"]
})
export class EmployeeSalaryComponent implements OnInit {
    constructor(public salaryService: SalaryService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}
    salaries: PaginatedResult<Salary>;
    salariesColumns: TableColumn[];
    salariesParams = new SearchParams();
    searchString: string;
    actionButtons: CustomAction[] = [
        new CustomAction("Increament", "Increament", "Update", "trending_up"),
        new CustomAction("Decrement", "Decrement", "Update", "trending_down", "warn"),
        // new CustomAction("Incentives", "Incentives", "Update", "bolt"),
        // new CustomAction("Deductions", "Deductions", "Update", "trending_down", "warn")
    ];

    ngOnInit(): void {
        this.getAllSalaries();
        this.initColumns();
    }
    getAllSalaries() {
        this.salaryService.getAll(this.salariesParams).subscribe((res) => {
            this.salaries = res;
        });
    }
    initColumns(): void {
        this.salariesColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true },
            { name: "Basic Salary", dataKey: "basicSalary", isSortable: true, isShowable: true },
            { name: "Current Salary", dataKey: "currentSalary", isSortable: true, isShowable: true },
            { name: "Incentive", dataKey: "incentive", isSortable: true, isShowable: true },
            { name: "Deductions", dataKey: "deduction", isSortable: true, isShowable: true },
            { name: "Payable Salary", dataKey: "payableSalary", isSortable: true, isShowable: true },
            { name: "Salary/Day", dataKey: "perDaySalary", isSortable: true, isShowable: true },
            { name: "Salary/Hour", dataKey: "perHourSalary", isSortable: true, isShowable: true },
            { name: "Days In Month", dataKey: "totalDaysInMonth", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Increament","Decrement"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.salariesParams.pageNumber = event.pageNumber;
        this.salariesParams.pageSize = event.pageSize;
        this.getAllSalaries();
    }

    openForm(customer?: Salary): void {}
    remove($event: string): void {
        this.salaryService.delete($event).subscribe(() => {
            this.getAllSalaries();
            this.toastr.info("Overtime Removed");
        });
    }

    sort($event: Sort): void {
        this.salariesParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.salariesParams.orderBy);
        this.getAllSalaries();
    }

    filter($event: string): void {
        this.salariesParams.searchString = $event.trim().toLocaleLowerCase();
        this.salariesParams.pageNumber = 0;
        this.salariesParams.pageSize = 0;
        this.getAllSalaries();
    }

    reload(): void {
        this.salariesParams.searchString = "";
        this.salariesParams.pageNumber = 0;
        this.salariesParams.pageSize = 0;
        this.getAllSalaries();
    }
    openCustomActionButton(data: any): void {
        console.log(data.button.key);
        if (data.button.key == "Increament") {
            data.type = SalaryPerksType.Increment;
        } else if (data.button.key == "Decrement") {
            data.type = SalaryPerksType.Decrement;
        } else if (data.button.key == "Incentives") {
            data.type = SalaryPerksType.Incentives;
        } else if (data.button.key == "Deductions") {
            data.type = SalaryPerksType.Deductions;
        }
        this.openIncreamentForm(data);
    }

    openIncreamentForm(rowData: any) {
        const dialogRef = this.dialog.open(IncrementDecrementFormComponent, {
            data: rowData
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAllSalaries();
            }
        });
    }
}

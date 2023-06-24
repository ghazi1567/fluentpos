import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { SalaryPerksType, SalaryPerksTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Salary } from "../../models/salary";
import { SearchParams } from "../../models/SearchParams";
import { SalaryService } from "../../services/salary.service";
import { IncrementDecrementFormComponent } from "./increment-decrement-form/increment-decrement-form.component";
import { PerksComponent } from "./perks/perks.component";

@Component({
    selector: "app-employee-salary",
    templateUrl: "./employee-salary.component.html",
    styleUrls: ["./employee-salary.component.scss"]
})
export class EmployeeSalaryComponent implements OnInit {
    constructor(public salaryService: SalaryService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}
    salaries: PaginatedResult<Salary>;
    salariesData: Salary[];
    salariesColumns: any[];
    salariesParams = new SearchParams();
    searchString: string;
    actionButtons: CustomAction[] = [
        new CustomAction("Increament", "Increament", "Update", "trending_up"),
        new CustomAction("Decrement", "Decrement", "Update", "trending_down", "warn"),
        new CustomAction("Incentives", "Incentives", "Update", "bolt"),
        new CustomAction("Deductions", "Deductions", "Update", "trending_down", "warn")
    ];

    ngOnInit(): void {
        this.salariesData = [];
        this.getAllSalaries();
        this.initColumns();
    }
    getAllSalaries() {
        this.salariesParams.pageNumber = 0;
        this.salariesParams.pageSize = 1000;
        this.salaryService.getAll(this.salariesParams).subscribe((res) => {
            this.salaries = res;
            this.salariesData = res.data;
        });
    }
    initColumns1(): void {
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
            { name: "Action", dataKey: "action", position: "right", buttons: ["Increament", "Decrement", "Incentives", "Deductions"] }
        ];
    }
    initColumns(): void {
        this.salariesColumns = [
            // { headerName: "Id", field: "Id", sortable: true },
            { headerName: "Employee Name", field: "employeeName", sortable: true },
            { headerName: "Basic Salary", field: "basicSalary", sortable: true },
            { headerName: "Current Salary", field: "currentSalary", sortable: true },
            { headerName: "Incentive", field: "incentive", sortable: true },
            { headerName: "Deduction", field: "deduction", sortable: true },
            { headerName: "Payable Salary", field: "payableSalary", sortable: true },
            { headerName: "Salary/Day", field: "perDaySalary", sortable: true },
            { headerName: "Salary/Hour", field: "perHourSalary", sortable: true },
            { headerName: "Total Days In Month", field: "totalDaysInMonth", sortable: true },
            {
                headerName: "Edit",
                cellRenderer: "buttonRenderer",
                cellRendererParams: {
                    buttons: ["Increament", "Decrement", "Incentives", "Deductions"],
                    actionButtons: this.actionButtons,
                    onClick: this.onSaveButtonClick.bind(this)
                },
                width: 50,
                pinned: "right"
            }
        ];
    }

    onSaveButtonClick(params) {
        console.log(params);
        this.openCustomActionButton(params);
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
        if (data.type == SalaryPerksType.Increment || data.type == SalaryPerksType.Decrement) {
            this.openIncreamentForm(data);
        }
        if (data.type == SalaryPerksType.Incentives || data.type == SalaryPerksType.Deductions) {
            this.openIncentiveForm(data);
        }
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
    openIncentiveForm(rowData: any) {
        const dialogRef = this.dialog.open(PerksComponent, {
            data: rowData
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAllSalaries();
            }
        });
    }

    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridReady(event): void {
        if (this.AgGrid) {
            // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
        }
        this.getAllSalaries();
    }
}

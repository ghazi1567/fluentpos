import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { Employee } from "../../models/employee";
import { EmployeeService } from "../../services/employee.service";
import { EmployeeFormComponent } from "./employee-form/employee-form.component";

@Component({
    selector: "app-employee",
    templateUrl: "./employee.component.html",
    styleUrls: ["./employee.component.scss"]
})
export class EmployeeComponent implements OnInit {
    employees: PaginatedResult<Employee>;
    employeeColumns: TableColumn[];
    customerParams = new SearchParams();
    searchString: string;

    constructor(public employeeService: EmployeeService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getEmployees();
        this.initColumns();
    }

    getEmployees(): void {
        this.employeeService.getEmployees(this.customerParams).subscribe((result) => {
            this.employees = result;
        });
    }

    initColumns(): void {
        this.employeeColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "Full Name", dataKey: "fullName", isSortable: true, isShowable: true },
            { name: "Employee Code", dataKey: "employeeCode", isSortable: true, isShowable: true },
            { name: "Punch Code", dataKey: "punchCode", isSortable: true, isShowable: true },
            { name: "Employee Status", dataKey: "employeeStatus", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.customerParams.pageNumber = event.pageNumber;
        this.customerParams.pageSize = event.pageSize;
        this.getEmployees();
    }

    openForm(customer?: Employee): void {
        const dialogRef = this.dialog.open(EmployeeFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getEmployees();
            }
        });
    }

    remove($event: string): void {
        this.employeeService.deleteEmployee($event).subscribe(() => {
            this.getEmployees();
            this.toastr.info("Employee Removed");
        });
    }

    sort($event: Sort): void {
        this.customerParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.customerParams.orderBy);
        this.getEmployees();
    }

    filter($event: string): void {
        this.customerParams.searchString = $event.trim().toLocaleLowerCase();
        this.customerParams.pageNumber = 0;
        this.customerParams.pageSize = 0;
        this.getEmployees();
    }

    reload(): void {
        this.customerParams.searchString = "";
        this.customerParams.pageNumber = 0;
        this.customerParams.pageSize = 0;
        this.getEmployees();
    }
}

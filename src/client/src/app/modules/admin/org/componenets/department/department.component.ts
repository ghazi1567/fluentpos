import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Branch } from "../../models/branch";
import { Department } from "../../models/Department";
import { SearchParams } from "../../models/SearchParams";
import { DepartmentService } from "../../services/department.service";
import { branchFormComponent } from "../branch/branch-form/branch-form.component";
import { DepartmentFormComponent } from "./department-form/department-form.component";

@Component({
    selector: "app-department",
    templateUrl: "./department.component.html",
    styleUrls: ["./department.component.scss"]
})
export class DepartmentComponent implements OnInit {
    departments: PaginatedResult<Department>;
    departmentColumns: TableColumn[];
    departmentParams = new SearchParams();
    searchString: string;

    constructor(public departmentService: DepartmentService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getDepartments();
        this.initColumns();
    }

    getDepartments(): void {
        this.departmentService.getAlls(this.departmentParams).subscribe((result) => {
            this.departments = result;
        });
    }

    initColumns(): void {
        this.departmentColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "Description", dataKey: "description", isSortable: true, isShowable: true },
            // { name: "HOD", dataKey: "headOfDepartment", isSortable: true, isShowable: true },
            { name: "Production", dataKey: "production", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.departmentParams.pageNumber = event.pageNumber;
        this.departmentParams.pageSize = event.pageSize;
        this.getDepartments();
    }

    openForm(Branch?: Branch): void {
        const dialogRef = this.dialog.open(DepartmentFormComponent, {
            data: Branch
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getDepartments();
        });
    }

    remove($event: string): void {
        this.departmentService.delete($event).subscribe(() => {
            this.getDepartments();
            this.toastr.info("Department Removed");
        });
    }

    sort($event: Sort): void {
        this.departmentParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.departmentParams.orderBy);
        this.getDepartments();
    }

    filter($event: string): void {
        this.departmentParams.searchString = $event.trim().toLocaleLowerCase();
        this.departmentParams.pageNumber = 0;
        this.departmentParams.pageSize = 0;
        this.getDepartments();
    }

    reload(): void {
        this.departmentParams.searchString = "";
        this.departmentParams.pageNumber = 0;
        this.departmentParams.pageSize = 0;
        this.getDepartments();
    }
}

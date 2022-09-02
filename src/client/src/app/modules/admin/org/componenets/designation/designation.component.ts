import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Department } from "../../models/Department";
import { Designation } from "../../models/designation";
import { SearchParams } from "../../models/SearchParams";
import { DepartmentService } from "../../services/department.service";
import { DesignationService } from "../../services/designation.service";
import { DesignationFormComponent } from "./designation-form/designation-form.component";

@Component({
    selector: "app-designation",
    templateUrl: "./designation.component.html",
    styleUrls: ["./designation.component.scss"]
})
export class DesignationComponent implements OnInit {
    designations: PaginatedResult<Designation>;
    departments: PaginatedResult<Department>;
    designationColumns: TableColumn[];
    designationParams = new SearchParams();
    searchString: string;

    constructor(public departmentService: DepartmentService, public designationService: DesignationService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getDepartments();
        this.initColumns();
    }

    getDepartments(): void {
        this.departmentService.getAlls(this.designationParams).subscribe((result) => {
            this.departments = result;
            this.getDesignations();
        });
    }
    getDepartmentName(departmentId: any) {
        var department = this.departments.data.find((x) => x.id == departmentId);
        if (department) {
            return department.name;
        }
        return "";
    }

    getDesignations(): void {
        this.designationService.getAlls(this.designationParams).subscribe((result) => {
            this.designations = result;
            this.designations.data.forEach((x) => {
                x.departmentName = this.getDepartmentName(x.departmentId);
            });
        });
    }
    initColumns(): void {
        this.designationColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "Department", dataKey: "departmentName", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.designationParams.pageNumber = event.pageNumber;
        this.designationParams.pageSize = event.pageSize;
        this.getDesignations();
    }

    openForm(Branch?: Designation): void {
        const dialogRef = this.dialog.open(DesignationFormComponent, {
            data: Branch
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getDesignations();
        });
    }

    remove($event: string): void {
        this.designationService.delete($event).subscribe(() => {
            this.getDesignations();
            this.toastr.info("Department Removed");
        });
    }

    sort($event: Sort): void {
        this.designationParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.designationParams.orderBy);
        this.getDesignations();
    }

    filter($event: string): void {
        this.designationParams.searchString = $event.trim().toLocaleLowerCase();
        this.designationParams.pageNumber = 0;
        this.designationParams.pageSize = 0;
        this.getDesignations();
    }

    reload(): void {
        this.designationParams.searchString = "";
        this.designationParams.pageNumber = 0;
        this.designationParams.pageSize = 0;
        this.getDesignations();
    }
}

import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { JobTypeMapping } from "src/app/core/enums/JobType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { environment } from "src/environments/environment";
import { Department } from "../../models/Department";
import { Job } from "../../models/Job";
import { SearchParams } from "../../models/SearchParams";
import { DepartmentService } from "../../services/department.service";
import { JobService } from "../../services/job.service";
import { DepartmentFormComponent } from "../department/department-form/department-form.component";
import { JobFormComponent } from "./job-form/job-form.component";
import { JobRunFormComponent } from "./job-run-form/job-run-form.component";

@Component({
    selector: "app-jobs",
    templateUrl: "./jobs.component.html",
    styleUrls: ["./jobs.component.scss"]
})
export class JobsComponent implements OnInit {
    jobs: PaginatedResult<Job>;
    jobColumns: TableColumn[];
    departmentParams = new SearchParams();
    searchString: string;
    JobTypeMapping = JobTypeMapping;
    actionButtons: CustomAction[] = [new CustomAction("Configure", "configure", "Update", "check"), new CustomAction("Run Now", "run", "Update", "check")];
    customActionData: CustomAction = new CustomAction("Jobs Dashboard", "dashboard", "update", "AutoGraph");
    constructor(public jobService: JobService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getDepartments();
        this.initColumns();
    }

    getDepartments(): void {
        this.jobService.getAlls(this.departmentParams).subscribe((result) => {
            this.jobs = result;
            this.jobs.data.forEach((x) => {
                x.job = JobTypeMapping[x.jobName];
            });
        });
    }

    initColumns(): void {
        this.jobColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "Job Name", dataKey: "job", isSortable: true, isShowable: true },
            { name: "Schedule", dataKey: "schedule", isSortable: true, isShowable: true },
            { name: "Enabled", dataKey: "enabled", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register","configure", "run", "Update"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.departmentParams.pageNumber = event.pageNumber;
        this.departmentParams.pageSize = event.pageSize;
        this.getDepartments();
    }

    openForm(Branch?: any): void {
        const dialogRef = this.dialog.open(JobFormComponent, {
            data: Branch
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getDepartments();
        });
    }

    remove($event: string): void {
        this.jobService.delete($event).subscribe(() => {
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

    openCustomActionButton(data: any): void {
        const dialogRef = this.dialog.open(JobRunFormComponent, {
            data: data
        });
        dialogRef.afterClosed().subscribe((result) => {});
    }

    onDashBoardClick($event) {
        window.open(environment.jobDashboardUrl,'');
    }
}

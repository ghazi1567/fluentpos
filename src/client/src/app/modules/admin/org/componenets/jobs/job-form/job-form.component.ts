import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { JobType, JobTypeMapping } from "src/app/core/enums/JobType";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { Department } from "../../../models/Department";
import { Job } from "../../../models/Job";
import { SearchParams } from "../../../models/SearchParams";
import { DepartmentService } from "../../../services/department.service";
import { JobService } from "../../../services/job.service";
import { OrgService } from "../../../services/org.service";

@Component({
    selector: "app-job-form",
    templateUrl: "./job-form.component.html",
    styleUrls: ["./job-form.component.scss"]
})
export class JobFormComponent implements OnInit {
    jobForm: FormGroup;
    formTitle: string;
    hodLookups: any[];
    JobTypeMapping = JobTypeMapping;
    public jobTypes = Object.values(JobType).filter((value) => typeof value === "number");

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Job,
        private dialogRef: MatDialog,
        private jobService: JobService,
        private orgService: OrgService,
        private toastr: ToastrService,
        private fb: FormBuilder
    ) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
    }
    loadLookups() {
        let model = <SearchParams>{
            pageSize: 10000
        };
        this.orgService.getAlls(model).subscribe((res) => {
            this.hodLookups = res.data;
        });
    }
    cronForm: any;
    initializeForm() {
        this.cronForm = new FormControl("0 0 1/1 * *");
        this.jobForm = this.fb.group({
            id: [this.data && this.data.id],
            jobName: [this.data && this.data.jobName, Validators.required],
            schedule: [this.data && this.data.schedule],
            enabled: [this.data && this.data.enabled || false],
            cron: []
        });
        if (this.jobForm.get("id").value === "" || this.jobForm.get("id").value == null) {
            this.formTitle = "Register Job";
        } else {
            this.formTitle = "Edit Job";
        }
    }

    onSubmit() {
        if (this.jobForm.valid) {
            var model = this.jobForm.value;
            model.schedule = this.cronForm.value;

            if (this.jobForm.get("id").value === "" || this.jobForm.get("id").value == null) {
                this.jobService.create(this.jobForm.value).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.success(element);
                        });
                    }
                );
            } else {
                this.jobService.update(this.jobForm.value).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.success(element);
                        });
                    }
                );
            }
        }
    }
}

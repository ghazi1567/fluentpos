import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { JobTypeMapping, JobType } from "src/app/core/enums/JobType";
import { Job } from "../../../models/Job";
import { SearchParams } from "../../../models/SearchParams";
import { JobService } from "../../../services/job.service";
import { OrgService } from "../../../services/org.service";

@Component({
    selector: "app-job-run-form",
    templateUrl: "./job-run-form.component.html",
    styleUrls: ["./job-run-form.component.scss"]
})
export class JobRunFormComponent implements OnInit {
    jobForm: FormGroup;
    formTitle: string;
    hodLookups: any[];
    JobTypeMapping = JobTypeMapping;
    public jobTypes = Object.values(JobType).filter((value) => typeof value === "number");

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
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
        this.jobForm = this.fb.group({
            id: [this.data && this.data.event.id],
            date: [new Date()]
        });
        if (this.jobForm.get("id").value === "" || this.jobForm.get("id").value == null) {
            this.formTitle = "Run Job";
        } else {
            this.formTitle = "Run Job";
        }
    }

    onSubmit() {
        if (this.jobForm.valid) {
            var model = this.jobForm.value;
            model.IsConfigure = this.data.button.key == "configure";

            this.jobService.runJob(model).subscribe(
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

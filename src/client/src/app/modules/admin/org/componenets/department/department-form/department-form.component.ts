import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormGroup, UntypedFormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { map } from "rxjs/operators";
import { LookupApiService } from "src/app/core/api/common/lookup.service";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Department } from "../../../models/department";
import { Organization } from "../../../models/organization";
import { Policy } from "../../../models/policy";
import { SearchParams } from "../../../models/SearchParams";
import { DepartmentService } from "../../../services/department.service";
import { OrgService } from "../../../services/org.service";
import { PolicyService } from "../../../services/policy.service";

@Component({
    selector: "app-department-form",
    templateUrl: "./department-form.component.html",
    styleUrls: ["./department-form.component.scss"]
})
export class DepartmentFormComponent implements OnInit {
    departmentForm: UntypedFormGroup;
    formTitle: string;
    hodLookups: any[];
    policies: any[];
    departments: any[];
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Department,
        private dialogRef: MatDialog,
        private departmentService: DepartmentService,
        private policyService: PolicyService,
        private orgService: OrgService,
        private toastr: ToastrService,
        private fb: UntypedFormBuilder,
        private lookupApiService: LookupApiService
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
        this.lookupApiService
            .getPolicyLookup()
            .subscribe((res) => {
                this.policies = res.data;
            });
        this.lookupApiService
            .getDepartmentLookup()
            .subscribe((result) => {
                this.departments = result.data;
            });
    }
    initializeForm() {
        this.departmentForm = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            policyId: [this.data && this.data.policyId, Validators.required],
            description: [this.data && this.data.description],
            headOfDepartment: [this.data && this.data.headOfDepartment],
            production: [this.data && this.data.production],
            parentId: [this.data && this.data.parentId]
        });
        if (this.departmentForm.get("id").value === "" || this.departmentForm.get("id").value == null) {
            this.formTitle = "Register Department";
        } else {
            this.formTitle = "Edit Department";
        }
    }

    onSubmit() {
        if (this.departmentForm.valid) {
            if (this.departmentForm.get("id").value === "" || this.departmentForm.get("id").value == null) {
                this.departmentService.create(this.departmentForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.departmentService.update(this.departmentForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

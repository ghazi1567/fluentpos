import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormGroup, UntypedFormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { LookupApiService } from "src/app/core/api/common/lookup.service";
import { Department } from "../../../models/Department";
import { Designation } from "../../../models/designation";
import { SearchParams } from "../../../models/SearchParams";
import { DepartmentService } from "../../../services/Department.service";
import { DesignationService } from "../../../services/designation.service";
import { OrgService } from "../../../services/org.service";

@Component({
    selector: "app-designation-form",
    templateUrl: "./designation-form.component.html",
    styleUrls: ["./designation-form.component.scss"]
})
export class DesignationFormComponent implements OnInit {
    designationForm: UntypedFormGroup;
    formTitle: string;
    departmentLookups: any[];
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Designation,
        private dialogRef: MatDialog,
        private departmentService: DepartmentService,
        private designationService: DesignationService,
        private toastr: ToastrService,
        private fb: UntypedFormBuilder,
        private lookupApiService: LookupApiService
    ) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
    }
    loadLookups() {
        this.lookupApiService.getDepartmentLookup().subscribe((res) => {
            this.departmentLookups = res.data;
        });
    }
    initializeForm() {
        this.designationForm = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            departmentId: [this.data && this.data.departmentId, Validators.required]
        });
        if (this.designationForm.get("id").value === "" || this.designationForm.get("id").value == null) {
            this.formTitle = "Register Designation";
        } else {
            this.formTitle = "Edit Designation";
        }
    }

    onSubmit() {
        if (this.designationForm.valid) {
            if (this.designationForm.get("id").value === "" || this.designationForm.get("id").value == null) {
                this.designationService.create(this.designationForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.designationService.update(this.designationForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

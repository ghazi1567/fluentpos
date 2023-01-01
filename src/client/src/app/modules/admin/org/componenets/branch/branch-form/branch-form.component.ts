import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormGroup, UntypedFormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Branch } from "../../../models/branch";
import { Organization } from "../../../models/organization";
import { SearchParams } from "../../../models/SearchParams";
import { BranchService } from "../../../services/branch.service";
import { OrgService } from "../../../services/org.service";

@Component({
    selector: "app-branch-form",
    templateUrl: "./branch-form.component.html",
    styleUrls: ["./branch-form.component.scss"]
})
export class branchFormComponent implements OnInit {
    branchForm: UntypedFormGroup;
    formTitle: string;
    orgLookups: Organization[];
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Branch,
        private dialogRef: MatDialog,
        private branchService: BranchService,
        private orgService: OrgService,
        private toastr: ToastrService,
        private fb: UntypedFormBuilder
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
            this.orgLookups = res.data;
        });
    }
    initializeForm() {
        this.branchForm = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            organizationId: [this.data && this.data.organizationId, Validators.required],
            address: [this.data && this.data.address],
            phoneNo: [this.data && this.data.phoneNo],
            emailAddress: [this.data && this.data.emailAddress],
            currency: [this.data && this.data.currency],
            country: [this.data && this.data.country]
        });
        if (this.branchForm.get("id").value === "" || this.branchForm.get("id").value == null) {
            this.formTitle = "Register Branch";
        } else {
            this.formTitle = "Edit Branch";
        }
    }

    onSubmit() {
        if (this.branchForm.valid) {
            if (this.branchForm.get("id").value === "" || this.branchForm.get("id").value == null) {
                this.branchService.create(this.branchForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.branchService.update(this.branchForm.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

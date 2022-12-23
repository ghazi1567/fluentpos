import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { AuthService } from "src/app/core/services/auth.service";
import { BusyService } from "src/app/core/services/busy.service";
import { Organization } from "../../models/organization";
import { OrgService } from "../../services/org.service";

@Component({
    selector: "app-setup",
    templateUrl: "./setup.component.html",
    styleUrls: ["./setup.component.scss"]
})
export class SetupComponent implements OnInit {
    form: UntypedFormGroup;
    formTitle: string;
    editMode = false;
    data: Organization = null;
    isLoading = true;
    constructor(private toastr: ToastrService, private fb: UntypedFormBuilder, private orgService: OrgService, private busyService: BusyService, private authService: AuthService) {}

    ngOnInit(): void {
        this.isLoading = true;
        this.getOrganizationById();
    }

    getOrganizationById() {
        this.busyService.isLoading.next(this.isLoading);

        this.orgService.getById(this.authService.getOrganizationId).subscribe(
            (res) => {
                this.data = res.data as Organization;
                this.editMode = true;
                this.isLoading = false;
                this.busyService.isLoading.next(this.isLoading);
                this.initializeForm();
            },
            (error) => {
                this.isLoading = false;
                this.editMode = false;
                this.busyService.isLoading.next(this.isLoading);
                this.initializeForm();
            }
        );
    }

    initializeForm() {
        this.form = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            address: [this.data && this.data.address, Validators.required],
            phoneNo: [this.data && this.data.phoneNo],
            emailAddress: [this.data && this.data.emailAddress],
            currency: [this.data && this.data.currency],
            country: [this.data && this.data.country]
        });
    }
    onSubmit() {
        let model = this.form.value;
        if (this.editMode) {
            this.orgService.update(model).subscribe((response) => {
                this.toastr.success(response.messages[0]);
            });
        } else {
            this.orgService.create(model).subscribe((response) => {
                this.toastr.success(response.messages[0]);
            });
        }
    }
}

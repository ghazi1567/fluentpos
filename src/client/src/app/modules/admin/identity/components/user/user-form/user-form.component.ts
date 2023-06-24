import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { EmployeeService } from "src/app/modules/admin/people/services/employee.service";
import { User } from "../../../models/user";
import { IdentityService } from "../../../services/identity.service";
import { UserService } from "../../../services/user.service";

@Component({
    selector: "app-user-form",
    templateUrl: "./user-form.component.html",
    styleUrls: ["./user-form.component.scss"]
})
export class UserFormComponent implements OnInit {
    userForm: UntypedFormGroup;
    formTitle: string;
    editMode = false;
    employeesLookup: Employee[];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: User,
        private employeeService: EmployeeService,
        private identityService: IdentityService,
        private userService: UserService,
        private toastr: ToastrService,
        private fb: UntypedFormBuilder
    ) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
        console.log(this.userForm.value);
    }

    loadLookups() {
        let employeeParams = new SearchParams();
        this.employeeService.getEmployeesLookup(employeeParams).subscribe((res) => {
            this.employeesLookup = res.data;
        });
    }

    initializeForm() {
        this.userForm = this.fb.group({
            id: [this.data && this.data.id],
            userName: [this.data && this.data.userName, Validators.required],
            firstName: [this.data && this.data.firstName, Validators.required],
            lastName: [this.data && this.data.lastName, Validators.required],
            email: [this.data && this.data.email, Validators.required],
            emailConfirmed: [true],
            password: [this.data && this.data.password],
            confirmPassword: [this.data && this.data.confirmPassword],
            phoneNumber: [this.data && this.data.phoneNumber, Validators.required],
            phoneNumberConfirmed: [true],
            employeeId: [this.data && this.data.employeeId],
        });
        if (this.userForm.get("id").value === "" || this.userForm.get("id").value == null) {
            this.formTitle = "Register User";
            this.editMode = false;
        } else {
            this.formTitle = "Edit User";
            this.editMode = true;
        }
    }

    onSubmit() {
        if (this.userForm.valid) {
            if (this.userForm.get("id").value === "" || this.userForm.get("id").value == null) {
                this.identityService.registerUser(this.userForm.value).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                    },
                    (error) => {
                        for (let err in error.errors) {
                            this.toastr.error(error.errors[err][0]);
                        }
                    }
                );
            } else {
                this.userService.updateUser(this.userForm.value).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.error(element);
                        });
                    }
                );
            }
        }
    }
}

import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { AuthService } from "src/app/core/services/auth.service";
import { Employee } from "../../../models/employee";
import { EmployeeRequest } from "../../../models/employeeRequest";
import { EmployeeService } from "../../../services/employee.service";
import { OvertimeService } from "../../../services/overtime.service";

@Component({
    selector: "app-overtime-form",
    templateUrl: "./overtime-form.component.html",
    styleUrls: ["./overtime-form.component.scss"]
})
export class OvertimeFormComponent implements OnInit {
    formGroup: FormGroup;
    formTitle: string;
    employeesLookup: Employee[];
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: EmployeeRequest,
        private dialogRef: MatDialog,
        private overtimeService: OvertimeService,
        private employeeService: EmployeeService,
        private toastr: ToastrService,
        private fb: FormBuilder,
        private authService: AuthService
    ) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
    }

    loadLookups() {
        let employeeParams = new SearchParams();
        this.employeeService.getEmployees(employeeParams).subscribe((res) => {
            this.employeesLookup = res.data;
        });
    }

    initializeForm() {
        this.formGroup = this.fb.group({
            id: [this.data && this.data.id],
            employeeId: [this.data && this.data.employeeId, Validators.required],
            attendanceDate: [this.data && this.data.attendanceDate, Validators.required],
            checkIn: [this.data && this.data.checkIn, Validators.required],
            checkOut: [this.data && this.data.checkOut, Validators.required],
            reason: [this.data && this.data.reason, Validators.required],
            status: [(this.data && this.data.status) || RequestStatus.Pending],
            requestType: [(this.data && this.data.requestType) || RequestType.OverTime],
            requestedBy: [(this.data && this.data.requestedBy) || this.authService.getEmployeeId]
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Register Overtime";
        } else {
            this.formTitle = "Edit Overtime";
        }
    }

    onSubmit() {
        if (this.formGroup.valid) {
            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.overtimeService.create(this.formGroup.value).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach((x) => {
                            this.toastr.error(x);
                        });
                    }
                );
            } else {
                var data = { ...this.data, ...this.formGroup.value };
                this.overtimeService.update(data).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach((x) => {
                            this.toastr.error(x);
                        });
                    }
                );
            }
        }
    }
}

import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { AuthService } from "src/app/core/services/auth.service";
import { Branch } from "src/app/modules/admin/org/models/branch";
import { Organization } from "src/app/modules/admin/org/models/organization";
import { SearchParams } from "src/app/modules/admin/org/models/SearchParams";
import { BranchService } from "src/app/modules/admin/org/services/branch.service";
import { OrgService } from "src/app/modules/admin/org/services/org.service";
import { Employee } from "../../../models/employee";
import { EmployeeRequest } from "../../../models/employeeRequest";
import { AttendanceService } from "../../../services/attendance.service";
import { EmployeeService } from "../../../services/employee.service";

@Component({
    selector: "app-attendance-form",
    templateUrl: "./attendance-form.component.html",
    styleUrls: ["./attendance-form.component.scss"]
})
export class AttendanceFormComponent implements OnInit {
    formGroup: FormGroup;
    formTitle: string;
    employeesLookup: Employee[];
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: EmployeeRequest,
        private dialogRef: MatDialog,
        private attendanceService: AttendanceService,
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
            requestType: [(this.data && this.data.requestType) || RequestType.Attendance],
            requestedBy: [(this.data && this.data.requestedBy) || this.authService.getEmployeeId]
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Register Attendance";
        } else {
            this.formTitle = "Edit Attendance";
        }
    }

    onSubmit() {
        if (this.formGroup.valid) {
            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.attendanceService.create(this.formGroup.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                var data = { ...this.data, ...this.formGroup.value };
                this.attendanceService.update(data).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

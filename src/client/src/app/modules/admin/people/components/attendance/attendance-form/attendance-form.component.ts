import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { AttendanceStatus, AttendanceStatusMapping } from "src/app/core/enums/AttendanceStatus";
import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { AuthService } from "src/app/core/services/auth.service";
import { Branch } from "src/app/modules/admin/org/models/branch";
import { Organization } from "src/app/modules/admin/org/models/organization";
import { SearchParams } from "src/app/modules/admin/org/models/SearchParams";
import { BranchService } from "src/app/modules/admin/org/services/branch.service";
import { OrgService } from "src/app/modules/admin/org/services/org.service";
import { Attendance } from "../../../models/Attendance";
import { Employee } from "../../../models/employee";
import { EmployeeRequest } from "../../../models/employeeRequest";
import { AttendanceRequestService } from "../../../services/attendance-request.service";
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
    public AttendanceStatusMapping = AttendanceStatusMapping;
    public attendanceStatus = Object.values(AttendanceStatus).filter((value) => typeof value === "number");

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Attendance,
        private dialogRef: MatDialog,
        private attendanceService: AttendanceService,
        private employeeService: EmployeeService,
        private toastr: ToastrService,
        private fb: FormBuilder,
        private authService: AuthService,
        private attendanceRequestService: AttendanceRequestService
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
            attendanceDate: [this.data && this.data.attendanceDate, Validators.required],
            attendanceStatus: [this.data && this.data.attendanceStatus, Validators.required],
            checkIn: [this.data && this.data.checkIn, Validators.required],
            checkOut: [this.data && this.data.checkOut, Validators.required],
            reason: [this.data && this.data.reason, Validators.required]
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
                data.modificationId = data.id;
                data.id = "";
                data.requestType = RequestType.AttendanceModify;
                data.requestedBy = this.authService.getEmployeeId;
                data.requestedBy = this.authService.getEmployeeId;
                this.attendanceRequestService.create(data).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
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

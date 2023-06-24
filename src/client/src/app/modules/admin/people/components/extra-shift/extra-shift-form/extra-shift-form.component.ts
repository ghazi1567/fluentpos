import { Component, Inject, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { EmployeeService } from "../../../services/employee.service";
import { Employee } from "../../../models/employee";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { AttendanceRequestService } from "../../../services/attendance-request.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { EmployeeRequest } from "../../../models/employeeRequest";
import * as moment from "moment";

@Component({
    selector: "app-extra-shift-form",
    templateUrl: "./extra-shift-form.component.html",
    styleUrls: ["./extra-shift-form.component.scss"]
})
export class ExtraShiftFormComponent implements OnInit {
    formGroup: FormGroup;
    employees: Employee[];
    policies: any[];
    model: any = {
        policyId: "",
        employeeIds: [],
        shiftDate: new Date()
    };
    title = "Shift Planner";
    minDate: Date;
    maxDate: Date;
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private dialogRef: MatDialog,
        public employeeService: EmployeeService,
        private fb: FormBuilder,
        public toastr: ToastrService,
        public attendanceRequestService: AttendanceRequestService
    ) {}

    ngOnInit(): void {
        // Set the minimum to January 1st 20 years in the past and December 31st a year in the future.
        this.minDate = new Date();
        this.initializeForm();
        this.loadLookups();
    }
    loadLookups() {
        let parms = new SearchParams();
        parms.pageSize = 1000;
        this.employeeService.getEmployeesLookup(parms).subscribe((res) => {
            this.employees = res.data;
        });
        this.employeeService.getPolicyLookup(parms).subscribe((res) => {
            this.policies = res.data;
        });
    }
    initializeForm() {
        var empIds = [];
        this.formGroup = this.fb.group({
            id: [this.data && this.data.id],
            employeeIds: [empIds, Validators.required],
            shiftDate: [new Date(), Validators.required],
            startTimeSpan: [Validators.required],
            endTimeSpan: [Validators.required],
            isNextDay: [false],
            policyId: [Validators.required]
        });
    }
    onSubmit() {
        var model = this.formGroup.value;
        if (model.employeeIds.length == 0) {
            this.toastr.error("Please select employees");
            return;
        }
        if (model.policyId == "") {
            this.toastr.error("Please select policy");
            return;
        }
        if (!model.shiftDate) {
            this.toastr.error("Please select shift date");
            return;
        }
        if (model.endTimeSpan < model.startTimeSpan && !model.isNextDay) {
            this.toastr.error("End time must be greater then start time. Please tick next day checkbox in case of next day checkout.");
            return;
        }

        this.attendanceRequestService.createShiftPlan(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.dialogRef.closeAll();
            }
        });
    }
}

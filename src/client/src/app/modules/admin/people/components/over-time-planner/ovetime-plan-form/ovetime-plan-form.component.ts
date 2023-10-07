import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { Employee } from "../../../models/employee";
import { AttendanceRequestService } from "../../../services/attendance-request.service";
import { EmployeeService } from "../../../services/employee.service";
import { OvertimeService } from "../../../services/overtime.service";

@Component({
    selector: "app-ovetime-plan-form",
    templateUrl: "./ovetime-plan-form.component.html",
    styleUrls: ["./ovetime-plan-form.component.scss"]
})
export class OvetimePlanFormComponent implements OnInit {
    formGroup: FormGroup;
    employees: Employee[];
    policies: any[];
    model: any = {
        policyId: "",
        employeeIds: [],
        overtimeDate: new Date()
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
        public overtimeService: OvertimeService
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
            overtimeDate: [new Date(), Validators.required],
            startTimeSpan: ["", Validators.required],
            endTimeSpan: ["", Validators.required],
            isNextDay: [false]
        });
    }
    onSubmit() {
        var model = this.formGroup.value;
        if (model.employeeIds.length == 0) {
            this.toastr.error("Please select employees");
            return;
        }

        if (!model.overtimeDate) {
            this.toastr.error("Please select shift date");
            return;
        }
        if (model.endTimeSpan < model.startTimeSpan && !model.isNextDay) {
            this.toastr.error("End time must be greater then start time. Please tick next day checkbox in case of next day checkout.");
            return;
        }

        this.overtimeService.createOvertimePlan(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.dialogRef.closeAll();
            }
        });
    }
}

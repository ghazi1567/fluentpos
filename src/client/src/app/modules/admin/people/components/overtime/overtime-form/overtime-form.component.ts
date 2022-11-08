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
    departments: any[];
    showTime = true;
    disableProductionOption = true;
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
        this.employeeService.getDepartmentLookup(employeeParams).subscribe((res) => {
            this.departments = res.data;
        });
    }

    initializeForm() {
        this.formGroup = this.fb.group({
            id: [this.data && this.data.id],
            employeeId: [this.data && this.data.employeeId, Validators.required],
            departmentId: [this.data && this.data.departmentId, Validators.required],
            attendanceDate: [this.data && this.data.attendanceDate, Validators.required],
            checkIn: [this.data && this.data.checkIn],
            checkOut: [this.data && this.data.checkOut],
            reason: [this.data && this.data.reason, Validators.required],
            status: [(this.data && this.data.status) || RequestStatus.Pending],
            requestType: [(this.data && this.data.requestType) || RequestType.OverTime],
            requestedBy: [(this.data && this.data.requestedBy) || this.authService.getEmployeeId],
            production: [0],
            requiredProduction: [0],
            overtimeType: []
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Register Overtime";
        } else {
            this.formTitle = "Edit Overtime";
        }
    }

    onSubmit() {
        if (this.formGroup.valid) {
            var model = this.formGroup.value;
            if (model.overtimeType == 1) {
                if (!model.checkIn || !model.checkOut) {
                    this.toastr.error("Please enter check in/out time.");
                    return;
                }
            }
            if (model.overtimeType == 2) {
                if (!model.production || model.production == 0) {
                    this.toastr.error("Please enter product qty.");
                    return;
                }
                var perHourQty = parseInt(model.requiredProduction) / 8;
                var overtimeHours = parseInt(model.production) / perHourQty;
                var outTimeTime = 9 + overtimeHours;


                model.checkIn = "09:00";
                model.checkOut = parseInt(outTimeTime.toFixed(0)) + ":00";
            }

            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.overtimeService.create(model).subscribe(
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
                var data = { ...this.data, ...model };
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

    departmentSelection(event) {
        //this.filteredDesignations = this.designations.filter((x) => x.departmentId == event.value);
        var department = this.departments.find((x) => x.id == event.value);
        if (department) {
            this.disableProductionOption = department.production == 0;
            this.formGroup.get("requiredProduction").setValue(department.production);
        }
    }
    overtimeTypeSelection(event) {
        this.showTime = event.value == 1;
    }
    employeeSelection(event) {
        console.log(event);
        var employee = this.employeesLookup.find((x) => x.id == event.value);
        if (employee) {
            this.formGroup.get("departmentId").setValue(employee.departmentId);
            this.departmentSelection({
                value: employee.departmentId
            });
        }
    }

    get departmentName() {
        return this.formGroup.get("departmentId");
    }
}

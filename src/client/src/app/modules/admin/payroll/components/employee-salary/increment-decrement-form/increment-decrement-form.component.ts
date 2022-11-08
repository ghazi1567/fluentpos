import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { SalaryPerksType, SalaryPerksTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { AuthService } from "src/app/core/services/auth.service";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { EmployeeRequest } from "src/app/modules/admin/people/models/employeeRequest";
import { AttendanceService } from "src/app/modules/admin/people/services/attendance.service";
import { EmployeeService } from "src/app/modules/admin/people/services/employee.service";
import { Salary } from "../../../models/salary";
import { SearchParams } from "../../../models/SearchParams";
import { SalaryPerksService } from "../../../services/salary-perks.service";

@Component({
    selector: "app-increment-decrement-form",
    templateUrl: "./increment-decrement-form.component.html",
    styleUrls: ["./increment-decrement-form.component.scss"]
})
export class IncrementDecrementFormComponent implements OnInit {
    formGroup: FormGroup;
    formTitle: string;
    employeesLookup: Employee[];

    constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialog, 
    private salaryPerksService: SalaryPerksService, private toastr: ToastrService, private fb: FormBuilder) {}

    ngOnInit(): void {
        this.initializeForm();
    }

    initializeForm() {
        this.formGroup = this.fb.group({
            id: [],
            employeeId: [this.data && this.data.event.employeeId, Validators.required],
            type: [this.data && this.data.type, Validators.required],
            percentage: [Validators.required],
            amount: [Validators.required],
            currentSalary: [{ value: this.data && this.data.event.currentSalary, disabled: true }],
            expectedSalary: [{ value: this.data && this.data.event.currentSalary, disabled: true }],
            isRecursion: [true],
            isRecursionUnLimited: [true],
            recursionEndMonth: [],
            description: [],
            isTaxable: [false],
            effecitveFrom: [Validators.required]
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Register " + SalaryPerksTypeMapping[this.data.type];
        } else {
            this.formTitle = "Edit " + SalaryPerksTypeMapping[this.data.type];
        }
    }

    onPercentageChange($event) {
        console.log($event.target.value);

        var amount = (($event.target.value / 100) * this.data.event.currentSalary).toFixed(2);
        var expectedSalary = this.data.event.currentSalary;

        if (this.data.type == SalaryPerksType.Increment) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) + parseFloat(amount)).toFixed(2);
        } else if (this.data.type == SalaryPerksType.Decrement) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) - parseFloat(amount)).toFixed(2);
        }
        this.formGroup.patchValue({
            amount: amount,
            expectedSalary: expectedSalary
        });
    }

    onAmountChange($event) {
        console.log($event.target.value);
        var percentage = (($event.target.value / this.data.event.currentSalary) * 100).toFixed(2);
        var expectedSalary = this.data.event.currentSalary;

        if (this.data.type == SalaryPerksType.Increment) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) + parseFloat($event.target.value)).toFixed(2);
        } else if (this.data.type == SalaryPerksType.Decrement) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) - parseFloat($event.target.value)).toFixed(2);
        }

        this.formGroup.patchValue({
            percentage: percentage,
            expectedSalary: expectedSalary
        });
    }

    onSubmit() {
        if (this.formGroup.valid) {
            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.salaryPerksService.create(this.formGroup.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.salaryPerksService.update(this.formGroup.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

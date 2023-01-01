import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import * as moment from "moment";
import { ToastrService } from "ngx-toastr";
import { Months, MonthsMapping } from "src/app/core/enums/Months";
import { PayPeriod, PayPeriodMapping } from "src/app/core/enums/PayPeriod";
import { SalaryPerksTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { PayrollService } from "../../../services/payroll.service";

@Component({
    selector: "app-payroll-request-form",
    templateUrl: "./payroll-request-form.component.html",
    styleUrls: ["./payroll-request-form.component.scss"]
})
export class PayrollRequestFormComponent implements OnInit {
    formGroup: UntypedFormGroup;
    formTitle: string;
    public PayPeriodMapping = PayPeriodMapping;
    public payPeriods = Object.values(PayPeriod).filter((value) => typeof value === "number");
    public MonthsMapping = MonthsMapping;
    public months = Object.values(Months).filter((value) => typeof value === "number");
    constructor(private fb: UntypedFormBuilder, @Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialog, private payrollService: PayrollService, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.initializeForm();
    }

    initializeForm() {
        var date = new Date();
        const startOfMonth: moment.Moment = moment().startOf("month");
        const endOfMonth: moment.Moment = moment().endOf("month");
        console.log(startOfMonth);
        console.log(endOfMonth);
        this.formGroup = this.fb.group({
            id: [],
            startDate: [startOfMonth, Validators.required],
            endDate: [endOfMonth, Validators.required],
            month: [new Date().getMonth() + 1, Validators.required],
            payPeriod: [PayPeriod.Monthly, Validators.required],
            ignoreAttendance: [false],
            ignoreDeductionForAbsents: [false],
            ignoreDeductionForLateComer: [false],
            ignoreOvertime: [false],
            status: ["Pending"]
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Generate Payroll Request ";
        } else {
            this.formTitle = "Edit ";
        }
    }
    onSubmit() {
        if (this.formGroup.valid) {
            var model = this.formGroup.value;
            model.startDate = this.payrollService.getDate(model.startDate);
            model.endDate = this.payrollService.getDate(model.endDate);
            var startMonth = parseInt(model.startDate.format("M"));
            var endMonth = parseInt(model.endDate.format("M"));

            if (startMonth != endMonth) {
                this.toastr.error("Start & End Date must be from same month.");
                return;
            }

            if (model.month != startMonth) {
                this.toastr.error("Start & End Date must be from select month.");
                return;
            }

            var dt = new Date();
            var currentMonth = new Date(dt.getFullYear(), parseInt(model.month) - 1, 1);
            
            var tempStartOfMonth = moment(currentMonth).startOf("month");
            var tempEndOfMonth = moment(currentMonth).endOf("month");



            var startOfMonth = moment(model.startDate).startOf("month");
            var endOfMonth = moment(model.endDate).endOf("month");

            if (model.payPeriod == PayPeriod.Monthly && (startOfMonth.format("YYYY-MM-DD") != tempStartOfMonth.format("YYYY-MM-DD") || endOfMonth.format("YYYY-MM-DD") != tempEndOfMonth.format("YYYY-MM-DD") )) {
                this.toastr.error("Please select valid start & end date of the month.");
                return;
            } else if (model.payPeriod == PayPeriod.HalfMonth) {
                var startHalfDate = moment(model.startDate).format("YYYY-MM-DD");
                var endHalfDate = moment(model.endDate).format("YYYY-MM-DD");

                var _15HalfDate = startOfMonth.clone().date(15).format("YYYY-MM-DD");
                var _16HalfDate = startOfMonth.clone().date(16).format("YYYY-MM-DD");

                if (startHalfDate == startOfMonth.format("YYYY-MM-DD") && endHalfDate == _15HalfDate) {
                } else if (startHalfDate == _16HalfDate && endHalfDate == endOfMonth.format("YYYY-MM-DD")) {
                } else {
                    this.toastr.error("Please select valid 1st or 2nd half of the month.");
                    return;
                }
            }

            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.payrollService.create(model).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.payrollService.update(model).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
}

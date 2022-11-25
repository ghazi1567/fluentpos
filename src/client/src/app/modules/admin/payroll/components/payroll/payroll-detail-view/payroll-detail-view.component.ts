import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ActivatedRoute } from "@angular/router";
import * as moment from "moment";
import { ToastrService } from "ngx-toastr";
import { Months, MonthsMapping } from "src/app/core/enums/Months";
import { PaymentModeMapping } from "src/app/core/enums/PaymentMode";
import { PayPeriod, PayPeriodMapping } from "src/app/core/enums/PayPeriod";
import { SalaryPerksTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { TransactionType } from "src/app/core/enums/TransactionType";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Payroll } from "../../../models/Payroll";
import { PayrollService } from "../../../services/payroll.service";
import { PayslipService } from "../../../services/payslip.service";

@Component({
    selector: "app-payroll-detail-view",
    templateUrl: "./payroll-detail-view.component.html",
    styleUrls: ["./payroll-detail-view.component.scss"]
})
export class PayrollDetailViewComponent implements OnInit {
    formGroup: FormGroup;
    formTitle: string = "Payslip - ";
    public PayPeriodMapping = PayPeriodMapping;
    public payPeriods = Object.values(PayPeriod).filter((value) => typeof value === "number");
    public MonthsMapping = MonthsMapping;
    public months = Object.values(Months).filter((value) => typeof value === "number");
    public PaymentModeMapping = PaymentModeMapping;

    // earnedColumns: TableColumn[];
    earnedColumns: string[] = ["transactionName", "earning"];
    deductionColumns: string[] = ["transactionName", "deduction"];

    // deductionColumns: TableColumn[];
    earnings: any[];
    deductions: any[];
    payslip: Payroll;
    data: any = {};
    
    constructor(private activatedroute:ActivatedRoute,private fb: FormBuilder, private dialogRef: MatDialog, public payslipService: PayslipService, private toastr: ToastrService) {
        this.data.id = this.activatedroute.snapshot.paramMap.get("id");
        this.data.employeeName = "Inam";
    }

    ngOnInit(): void {
        this.formTitle = "Payslip - " + this.data.employeeName;
        this.initDeductionColumns();
        this.initEarnedColumns();
        this.loadPayslip();
    }
    loadPayslip() {
        this.payslipService.getById(this.data.id).subscribe((res) => {
            this.payslip = res.data;
            this.earnings = res.data.transactions.filter((x) => x.transactionType == TransactionType.Earning || x.transactionType == TransactionType.Allowances).sort((a,b) => a.transactionType - b.transactionType);;
            this.deductions = res.data.transactions.filter((x) => x.transactionType == TransactionType.Deduction);
        });
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
            // this.formTitle = "Generate Payroll Request ";
        } else {
            // this.formTitle = "Edit ";
        }
    }
    onSubmit() {}
    initEarnedColumns(): void {
        // this.earnedColumns = [
        //     { name: "Id", dataKey: "id", isSortable: false, isShowable: false },
        //     { name: "Earnings", dataKey: "transactionName", isSortable: false, isShowable: true },
        //     { name: "Amount", dataKey: "earning", isSortable: false, isShowable: true }
        // ];
    }
    initDeductionColumns(): void {
        // this.deductionColumns = [
        //     { name: "Id", dataKey: "id", isSortable: false, isShowable: false },
        //     { name: "Deductions", dataKey: "transactionName", isSortable: false, isShowable: true },
        //     { name: "Amount", dataKey: "deduction", isSortable: false, isShowable: true }
        // ];
    }
    getTotalDeductions() {
        return this.payslip ? this.payslip.totalDeduction : 0;
    }
    getTotalEarned() {
        return this.payslip ? this.payslip.totalEarned + this.payslip.totalIncentive : 0;
    }
    getNetPay() {
        return this.payslip ? this.payslip.netPay : 0;
    }
}

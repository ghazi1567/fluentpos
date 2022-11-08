import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { MonthsMapping, Months } from "src/app/core/enums/Months";
import { PayPeriodMapping, PayPeriod } from "src/app/core/enums/PayPeriod";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Payroll } from "../../models/Payroll";
import { PayrollRequest } from "../../models/payrollRequest";
import { Salary } from "../../models/salary";
import { SearchParams } from "../../models/SearchParams";
import { PayrollService } from "../../services/payroll.service";
import { PayslipService } from "../../services/payslip.service";
import { SalaryService } from "../../services/salary.service";
import { PayrollDetailViewComponent } from "./payroll-detail-view/payroll-detail-view.component";
import { PayrollRequestFormComponent } from "./payroll-request-form/payroll-request-form.component";

@Component({
    selector: "app-payroll",
    templateUrl: "./payroll.component.html",
    styleUrls: ["./payroll.component.scss"]
})
export class PayrollComponent implements OnInit {
    constructor(public payrollService: PayrollService, public payslipService: PayslipService, 
        public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService,
        public router: Router) {}
    payrollRequests: PaginatedResult<PayrollRequest>;
    paySlips: PaginatedResult<Payroll>;
    payrollRequestsColumns: TableColumn[];
    payrollSlipsColumns: TableColumn[];
    payrollRequestsParams = new SearchParams();
    searchString: string;
    actionButtons: CustomAction[] = [new CustomAction("Run", "run", "Update", "run")];
    payslipActionButtons: CustomAction[] = [new CustomAction("View Payslip", "ViewPayslip", "View", "remove_red_eye")];
    public PayPeriodMapping = PayPeriodMapping;
    public payPeriods = Object.values(PayPeriod).filter((value) => typeof value === "number");
    public MonthsMapping = MonthsMapping;
    public months = Object.values(Months).filter((value) => typeof value === "number");
    ngOnInit(): void {
        this.getAllpayrollRequests();
        this.initColumns();
        this.initPayslipColumns();
    }
    getAllpayrollRequests() {
        this.payrollService.getAll(this.payrollRequestsParams).subscribe((res) => {
            this.payrollRequests = res;
            this.payrollRequests.data.forEach((x) => {
                x.monthName = MonthsMapping[x.month];
                x.payPeriodName = PayPeriodMapping[x.payPeriod];
                x.run = x.status == 'Pending' || x.status == 'Failed' ? false : true;
            });
        });
        this.payslipService.getAll(this.payrollRequestsParams).subscribe((res) => {
            this.paySlips = res;
        });
    }
    initColumns(): void {
        this.payrollRequestsColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Start Date", dataKey: "startDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "End Date", dataKey: "endDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "Month", dataKey: "monthName", isSortable: true, isShowable: true },
            { name: "Pay Period", dataKey: "payPeriodName", isSortable: true, isShowable: true },
            { name: "Ignore Attendance", dataKey: "ignoreAttendance", isSortable: true, isShowable: true, columnType: "bool" },
            { name: "Ignore Absent", dataKey: "ignoreDeductionForAbsents", isSortable: true, isShowable: true, columnType: "bool" },
            { name: "Ignore late Commer", dataKey: "ignoreDeductionForLateComer", isSortable: true, isShowable: true, columnType: "bool" },
            { name: "Ignore Overtime", dataKey: "ignoreOvertime", isSortable: true, isShowable: true, columnType: "bool" },
            { name: "Status", dataKey: "status", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register","run"] }
        ];
    }

    initPayslipColumns(): void {
        this.payrollSlipsColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true },
            { name: "Start Date", dataKey: "startDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "End Date", dataKey: "endDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "Required Days", dataKey: "requiredDays", isSortable: true, isShowable: true },
            { name: "Present Days", dataKey: "presentDays", isSortable: true, isShowable: true },
            { name: "Absent Days", dataKey: "absentDays", isSortable: true, isShowable: true },
            { name: "Earned Days", dataKey: "earnedDays", isSortable: true, isShowable: true },
            { name: "Salary", dataKey: "employeeSalary", isSortable: true, isShowable: true ,columnType: "currency"},
            { name: "Earned", dataKey: "totalEarned", isSortable: true, isShowable: true,columnType: "currency" },
            { name: "Incentive", dataKey: "totalIncentive", isSortable: true, isShowable: true,columnType: "currency" },
            { name: "Deduction", dataKey: "totalDeduction", isSortable: true, isShowable: true,columnType: "currency" },
            { name: "Overtime", dataKey: "totalOvertime", isSortable: true, isShowable: true,columnType: "currency" },
            { name: "Payable", dataKey: "netPay", isSortable: true, isShowable: true,columnType: "currency" },
            { name: "Action", dataKey: "action", position: "right", buttons: ["ViewPayslip"] }
        ];
    }
    pageChanged(event: PaginatedFilter): void {
        this.payrollRequestsParams.pageNumber = event.pageNumber;
        this.payrollRequestsParams.pageSize = event.pageSize;
        this.getAllpayrollRequests();
    }

    openForm(customer?: PayrollRequest): void {
        const dialogRef = this.dialog.open(PayrollRequestFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAllpayrollRequests();
            }
        });
    }

    remove($event: string): void {
        this.payrollService.delete($event).subscribe(() => {
            this.getAllpayrollRequests();
            this.toastr.info("Overtime Removed");
        });
    }

    sort($event: Sort): void {
        this.payrollRequestsParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.payrollRequestsParams.orderBy);
        this.getAllpayrollRequests();
    }

    filter($event: string): void {
        this.payrollRequestsParams.searchString = $event.trim().toLocaleLowerCase();
        this.payrollRequestsParams.pageNumber = 0;
        this.payrollRequestsParams.pageSize = 0;
        this.getAllpayrollRequests();
    }

    reload(): void {
        this.payrollRequestsParams.searchString = "";
        this.payrollRequestsParams.pageNumber = 0;
        this.payrollRequestsParams.pageSize = 0;
        this.getAllpayrollRequests();
    }
    openCustomActionButton(data: any): void {
        console.log(data.button.key);
        this.payrollService.runJob(data.event.id).subscribe((res) => {
            this.toastr.success("Job started.");
        });
    }

    openPayslipCustomActionButton(data: any): void {
        console.log(data.button.key);
        // this.payslipService.getById(data.event.id).subscribe((res) => {

        // });

        const url = this.router.serializeUrl(
            this.router.createUrlTree(['/admin/payroll/payslip',data.event.id])
          );
      
          window.open(url, '_blank');

        // const dialogRef = this.dialog.open(PayrollDetailViewComponent, {
        //     data: data.event
        // });
        // dialogRef.afterClosed().subscribe((result) => {
        //     if (result) {
        //         // this.getAllpayrollRequests();
        //     }
        // });
    }

    
}

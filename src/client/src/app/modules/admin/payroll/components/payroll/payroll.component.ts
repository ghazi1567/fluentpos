import { Component, OnInit, ViewChild } from "@angular/core";
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
import * as moment from "moment";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";

@Component({
    selector: "app-payroll",
    templateUrl: "./payroll.component.html",
    styleUrls: ["./payroll.component.scss"]
})
export class PayrollComponent implements OnInit {

    payslipsData: any[];
    payslipsColumns: any[];


    constructor(
        public payrollService: PayrollService,
        public payslipService: PayslipService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public authService: AuthService,
        public router: Router
    ) { }
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
                x.run = x.status == "Pending" || x.status == "Failed" ? false : true;
            });
        });

    }
    getAllPayslips() {
        this.payrollRequestsParams.pageNumber = 0;
        this.payrollRequestsParams.pageSize = 100000;
        this.payslipService.getAll(this.payrollRequestsParams).subscribe((res) => {
            this.paySlips = res;
            this.payslipsData = res.data;
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
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register"] }
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
        this.payslipService.delete($event).subscribe((res) => {
            this.getAllpayrollRequests();
            this.toastr.info(res.messages[0]);
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

        const url = this.router.serializeUrl(this.router.createUrlTree(["/admin/payroll/payslip", data.event.id]));

        window.open(url, "_blank");

        // const dialogRef = this.dialog.open(PayrollDetailViewComponent, {
        //     data: data.event
        // });
        // dialogRef.afterClosed().subscribe((result) => {
        //     if (result) {
        //         // this.getAllpayrollRequests();
        //     }
        // });
    }

    initPayslipColumns2(): void {
        this.payrollSlipsColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true },
            { name: "Start Date", dataKey: "startDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "End Date", dataKey: "endDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "Required Days", dataKey: "requiredDays", isSortable: true, isShowable: true },
            { name: "Present Days", dataKey: "presentDays", isSortable: true, isShowable: true },
            { name: "Absent Days", dataKey: "absentDays", isSortable: true, isShowable: true },
            { name: "Earned Days", dataKey: "earnedDays", isSortable: true, isShowable: true },
            { name: "Salary", dataKey: "employeeSalary", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Earned", dataKey: "totalEarned", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Incentive", dataKey: "totalIncentive", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Deduction", dataKey: "totalDeduction", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Overtime", dataKey: "totalOvertime", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Payable", dataKey: "netPay", isSortable: true, isShowable: true, columnType: "currency" },
            { name: "Action", dataKey: "action", position: "right", buttons: ["ViewPayslip", "Remove"] }
        ];
    }


    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridReady(event): void {
        if (this.AgGrid) {
            // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
        }
        this.getAllPayslips();
    }

    initPayslipColumns(): void {
        this.payslipsColumns = [
            { headerName: "Id", field: "id", sortable: true, isShowable: false },
            { headerName: "Employee Name", field: "employeeName", sortable: true, isShowable: true },
            {
                headerName: "Start Date", field: "startDate", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD");
                    }
                    return value;
                }
            },
            {
                headerName: "End Date", field: "endDate", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD");
                    }
                    return value;
                }
            },
            { headerName: "Required Days", field: "requiredDays", sortable: true, valueFormatter: params => params.data.requiredDays.toFixed(1), },
            { headerName: "Present Days", field: "presentDays", sortable: true, valueFormatter: params => params.data.requiredDays.toFixed(1), },
            { headerName: "Absent Days", field: "absentDays", sortable: true, valueFormatter: params => params.data.requiredDays.toFixed(1), },
            { headerName: "Earned Days", field: "earnedDays", sortable: true, valueFormatter: params => params.data.requiredDays.toFixed(1), },
            { headerName: "Salary", field: "employeeSalary", sortable: true, valueFormatter: params => params.data.employeeSalary.toFixed(2), },
            { headerName: "Earned", field: "totalEarned", sortable: true, valueFormatter: params => params.data.totalEarned.toFixed(2), },
            { headerName: "Incentive", field: "totalIncentive", sortable: true, valueFormatter: params => params.data.totalIncentive.toFixed(2), },
            { headerName: "Deduction", field: "totalDeduction", sortable: true, valueFormatter: params => params.data.totalDeduction.toFixed(2), },
            { headerName: "Overtime", field: "totalOvertime", sortable: true, valueFormatter: params => params.data.totalOvertime.toFixed(2), },
            { headerName: "Payable", field: "netPay", sortable: true, valueFormatter: params => params.data.netPay.toFixed(2), },
            {
                headerName: "Action",
                cellRenderer: "buttonRenderer",
                cellRendererParams: {
                    buttons: ["ViewPayslip", "Remove"],
                    actionButtons: this.payslipActionButtons,
                    onClick: this.openPayslipCustomActionButton.bind(this)
                },
                width: 50,
                pinned: "right"
            }


        ];
    }

    openDeleteConfirmationDialog($event: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the removal of this Overtime record.?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result.confirmed) {
                this.remove($event.data.id);
            }
        });
    }


}

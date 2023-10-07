import { Component, OnInit, ViewChild } from "@angular/core";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { DeleteDialogComponent } from "src/app/core/shared/components/delete-dialog/delete-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { AuthService } from "src/app/core/services/auth.service";
import { AttendanceRequestService } from "../../services/attendance-request.service";
import { OvertimeService } from "../../services/overtime.service";
import * as moment from "moment";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { OvetimePlanFormComponent } from "./ovetime-plan-form/ovetime-plan-form.component";

@Component({
    selector: "app-over-time-planner",
    templateUrl: "./over-time-planner.component.html",
    styleUrls: ["./over-time-planner.component.scss"]
})
export class OverTimePlannerComponent implements OnInit {
    overtimesParams = new PeopleSearchParams();
    overtimePlans: any[];
    overtimePlansColumns: any[];
    overtimes: any;
    actionButtons: CustomAction[] = [new CustomAction("Remove", "Remove", "Remove", "delete")];

    constructor(public overtimeService: OvertimeService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}

    ngOnInit(): void {
        this.initColumns();
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
        this.getShiftPlans();
    }

    getShiftPlans(): void {
        this.overtimesParams.pageSize = 10000;
        this.overtimeService.getOvertimePlans(this.overtimesParams).subscribe((result) => {
            this.overtimes = result;
            this.overtimePlans = result.data;
        });
    }

    openForm(customer?: any): void {
        const dialogRef = this.dialog.open(OvetimePlanFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getShiftPlans();
        });
    }
    openDeleteConfirmationDialog($event: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the removal of this Overtime record.?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.remove($event);
            }
        });
    }
    remove($event: any): void {
        this.overtimeService.deleteOvertimePlan($event.event.id).subscribe(() => {
            // this.getAttendances();
            this.toastr.info("Overtime Removed");
            this.getShiftPlans();
        });
    }
    handleReload() {}

    initColumns(): void {
        this.overtimePlansColumns = [
            { headerName: "Punch Code", field: "punchCode", sortable: true },
            { headerName: "Employee Name", field: "employeeName", sortable: true, width: 300 },
            {
                headerName: "Overtime Date",
                field: "overtimeDate",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD");
                    }
                    return value;
                }
            },
            {
                headerName: "Start Time",
                field: "startTimeSpan",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "hh:mm:ss a");
                    if (date.isValid()) {
                        value = date.format("hh:mm:ss a");
                    }
                    return value;
                },
                width: 250
            },
            {
                headerName: "End Time",
                field: "endTimeSpan",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "hh:mm:ss a");
                    if (date.isValid()) {
                        value = date.format("hh:mm:ss a");
                    }
                    return value;
                },
                width: 250
            },
            {
                headerName: "Next Day Checkout",
                field: "isNextDay",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;

                    if (value && value == true) {
                        return "Yes";
                    }
                    return "No";
                },
                width: 246
            },
            {
                headerName: "Actoion",
                cellRenderer: "buttonRenderer",
                cellRendererParams: {
                    buttons: ["Remove"],
                    actionButtons: this.actionButtons,
                    onClick: this.openDeleteConfirmationDialog.bind(this)
                },
                width: 50,
                pinned: "right"
            }
        ];
    }


}

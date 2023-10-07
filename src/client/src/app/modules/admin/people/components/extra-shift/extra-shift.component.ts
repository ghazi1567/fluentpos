import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { RequestStatusMapping, RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { ExtraShiftFormComponent } from "./extra-shift-form/extra-shift-form.component";
import { ExtraShiftRequest } from "../../models/extraShift";
import { AttendanceRequestService } from "../../services/attendance-request.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import * as moment from "moment";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";

@Component({
    selector: "app-extra-shift",
    templateUrl: "./extra-shift.component.html",
    styleUrls: ["./extra-shift.component.scss"]
})
export class ExtraShiftComponent implements OnInit {
    overtimes: PaginatedResult<ExtraShiftRequest>;
    overtimesColumns: TableColumn[];
    overtimesParams = new PeopleSearchParams();
    shiftPlans: any[];
    shiftPlansColumns: any[];
    searchString: string;
    actionButtons: CustomAction[] = [new CustomAction("Remove", "Remove", "Remove", "delete")];

    public RequestStatusMapping = RequestStatusMapping;
    constructor(public attendanceRequestService: AttendanceRequestService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}

    ngOnInit(): void {
        this.getShiftPlans();
        this.initColumns();
    }

    getShiftPlans(): void {
        this.overtimesParams.pageSize = 10000;
        this.attendanceRequestService.getShiftPlans(this.overtimesParams).subscribe((result) => {
            this.overtimes = result;
            this.shiftPlans = result.data;
            // this.overtimes.data.forEach((x) => {
            //     x.statusName = RequestStatusMapping[x.status];
            //     x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            //     x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.Pending || x.status == RequestStatus.Rejected;
            //     x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            // });
        });
    }

    initColumns(): void {
        // this.overtimesColumns = [
        //     { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
        //     { name: "Punch Code", dataKey: "punchCode", isSortable: true, isShowable: true },
        //     { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true },
        //     { name: "policyId", dataKey: "policyId", isSortable: true, isShowable: true },
        //     { name: "Shift Date", dataKey: "shiftDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
        //     { name: "Action", dataKey: "action", position: "right", buttons: ["Register", "Update", "Remove"] }
        // ];
        this.shiftPlansColumns = [
            // { headerName: "Id", field: "Id", sortable: true },
            { headerName: "Punch Code", field: "punchCode", sortable: true },
            { headerName: "Employee Name", field: "employeeName", sortable: true, width: 240, },
            { headerName: "Policy Name", field: "policyName", sortable: true },
            {
                headerName: "Shift Date",
                field: "shiftDate",
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
                }
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
                }
            },
            {
                headerName: "Next Day Checkout",
                field: "isNextDay",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                   
                    if (value && value == true) {
                        return 'Yes'
                    }
                    return 'No';
                }
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

    handleReload(): void {
        this.getShiftPlans();
    }

    openForm(customer?: ExtraShiftRequest): void {
        const dialogRef = this.dialog.open(ExtraShiftFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getShiftPlans();
        });
    }
 
    openDeleteConfirmationDialog($event: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the removal of this shift record.?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.remove($event);
            }
        });
    }
    remove($event: any): void {
        this.attendanceRequestService.deleteShiftPlan($event.event.id).subscribe(() => {
            // this.getAttendances();
            this.toastr.info("Shiftplan Removed");
            this.getShiftPlans();
        });
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
}

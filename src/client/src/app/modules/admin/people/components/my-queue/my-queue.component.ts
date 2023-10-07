import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { AttendanceStatusMapping } from "src/app/core/enums/AttendanceStatus";
import { RequestStatusMapping, RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType, RequestTypeMapping } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { AttendanceRequestService } from "../../services/attendance-request.service";
import { AttendanceService } from "../../services/attendance.service";
import { OvertimeService } from "../../services/overtime.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import * as moment from "moment";

@Component({
    selector: "app-my-queue",
    templateUrl: "./my-queue.component.html",
    styleUrls: ["./my-queue.component.scss"]
})
export class MyQueueComponent implements OnInit {
    attendances: PaginatedResult<EmployeeRequest>;
    attendanceColumns: TableColumn[];
    attendanceParams = new PeopleSearchParams();
    overtimes: PaginatedResult<EmployeeRequest>;
    // overtimeColumns: TableColumn[];
    overtimeParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;
    public AttendanceStatusMapping = AttendanceStatusMapping;
    public RequestTypeMapping = RequestTypeMapping;

    actionButtons: CustomAction[] = [new CustomAction("Approve", "approved", "Update", "check"), new CustomAction("Reject", "rejected", "Update", "close", "warn")];
    attendanceRequestPermission = ["Permissions.AttendanceRequests.MyQueue"];
    overtimeRequestPermission = ["Permissions.OvertimeRequests.MyQueue"];


    overtimesData: EmployeeRequest[] = [];
    overtimeColumns: any[];

    constructor(public attendanceService: AttendanceRequestService, public overtimeService: OvertimeService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) { }

    ngOnInit(): void {
        this.getAttendanceMyQueue();
        this.initAttendanceColumns();

        this.getOvertimeMyQueue();
        this.initOvertimeColumns();
    }

    getAttendanceMyQueue(): void {
        if (!this.authService.isAuthorized("Permission", this.attendanceRequestPermission)) {
            return;
        }
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceService.getMyQueue(this.attendanceParams).subscribe((result) => {
            this.attendances = result;
            this.attendances.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.requestTypeName = RequestTypeMapping[x.requestType];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
        });
    }

    getOvertimeMyQueue(): void {
        if (!this.authService.isAuthorized("Permission", this.overtimeRequestPermission)) {
            return;
        }
        this.overtimeParams.employeeId = this.authService.getEmployeeId;
        this.overtimeParams.requestType = RequestType.OverTime;
        this.overtimeParams.pageNumber = 0;
        this.overtimeParams.pageSize = 100000;
        this.overtimeService.getMyQueue(this.overtimeParams).subscribe((result) => {
            this.overtimes = result;
            this.overtimes.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.requestTypeName = RequestTypeMapping[x.requestType];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
            this.overtimesData = this.overtimes.data
        });
    }

    initAttendanceColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: false, isShowable: true },
            { name: "Request Type", dataKey: "requestTypeName", isSortable: false, isShowable: true },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "Attendance Status", dataKey: "attendanceStatusName", isSortable: false, isShowable: true },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["approved", "rejected"] }
        ];
    }
    initOvertimeColumnsOld(): void {
        this.overtimeColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Request Type", dataKey: "requestTypeName", isSortable: false, isShowable: true },
            { name: "Overtime Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["approved", "rejected"] }
        ];
    }

    pageChangedAttendance(event: PaginatedFilter): void {
        this.attendanceParams.pageNumber = event.pageNumber;
        this.attendanceParams.pageSize = event.pageSize;
        this.getAttendanceMyQueue();
    }
    pageChangedOvertime(event: PaginatedFilter): void {
        this.overtimeParams.pageNumber = event.pageNumber;
        this.overtimeParams.pageSize = event.pageSize;
        this.getOvertimeMyQueue();
    }

    sortAttendance($event: Sort): void {
        this.attendanceParams.orderBy = $event.active + " " + $event.direction;
        this.getAttendanceMyQueue();
    }
    sortOvertime($event: Sort): void {
        this.overtimeParams.orderBy = $event.active + " " + $event.direction;
        this.getOvertimeMyQueue();
    }

    filterAttendance($event: string): void {
        this.attendanceParams.searchString = $event.trim().toLocaleLowerCase();
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendanceMyQueue();
    }
    filterOvertime($event: string): void {
        this.overtimeParams.searchString = $event.trim().toLocaleLowerCase();
        this.overtimeParams.pageNumber = 0;
        this.overtimeParams.pageSize = 0;
        this.getOvertimeMyQueue();
    }

    reloadAttendance(): void {
        this.attendanceParams.searchString = "";
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendanceMyQueue();
    }
    reloadOvertime(): void {
        this.overtimeParams.searchString = "";
        this.overtimeParams.pageNumber = 0;
        this.overtimeParams.pageSize = 0;
        this.getOvertimeMyQueue();
    }

    openCustomActionButton(data: any): void {
        this.openDeleteConfirmationDialog(data);
    }
    openDeleteConfirmationDialog(data: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: {
                message: `Do you confirm the ${data.button.key} status of this record?`,
                showComments: true,
                commentLabel: "Comments",
                confirmButtonLabel: data.button.key,
                cancelButtonLabel: "Cancel",
                commentRequired: false,
                confirmColor: "primary",
                event: data.button.key,
                rowData: data.event
            }
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result.confirmed) {
                if (result.rowData.requestType == RequestType.Attendance || result.rowData.requestType == RequestType.AttendanceModify || result.rowData.requestType == RequestType.AttendanceDelete) {
                    this.updateAttendanceApproval(result);
                }
                if (result.rowData.requestType == RequestType.OverTime || result.rowData.requestType == RequestType.OverTimeModify || result.rowData.requestType == RequestType.OvertimeDelete) {
                    this.updateOvertimeApproval(result);
                }
            }
        });
    }

    updateAttendanceApproval(data) {
        var status;

        if (data.event == "rejected") {
            status = RequestStatus.Rejected;
        } else if (data.event == "approved") {
            status = RequestStatus.Approved;
        }
        var model = {
            Id: data.rowData.id,
            ApproverId: this.authService.getEmployeeId,
            Status: status,
            Comments: data.comments
        };

        this.attendanceService.updateApproval(model).subscribe(
            (res) => {
                this.toastr.success(res.messages[0]);
                this.getAttendanceMyQueue();
            },
            (error) => {
                this.toastr.error(error.messages[0]);
            }
        );
    }
    updateOvertimeApproval(data) {
        var status;

        if (data.event == "rejected") {
            status = RequestStatus.Rejected;
        } else if (data.event == "approved") {
            status = RequestStatus.Approved;
        }
        var model = {
            Id: data.rowData.id,
            ApproverId: this.authService.getEmployeeId,
            Status: status,
            Comments: data.comments
        };

        this.overtimeService.updateApproval(model).subscribe(
            (res) => {
                this.toastr.success(res.messages[0]);
                this.getOvertimeMyQueue();
            },
            (error) => {
                this.toastr.error(error.messages[0]);
            }
        );
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
        this.getOvertimeMyQueue();
    }

    initOvertimeColumns(): void {
        this.overtimeColumns = [
            { headerName: "Employee Name", field: "requestedForName", sortable: true, isShowable: true, width: 256 },
            {
                headerName: "Type", field: "overTimeType", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "hh:mm:ss a");
                    if (value == 1) {
                        value = 'Hour'
                    } else {
                        value = 'Production'
                    }
                    return value;
                },
                width: 120
            },
            {
                headerName: "Ovetime Date", field: "attendanceDate", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD");
                    }
                    return value;
                },
                width: 150
            },
            {
                headerName: "In Time", field: "checkIn", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 2) {
                        return '-';
                    }
                    let value = params.value;
                    let date = moment(value, "hh:mm:ss a");
                    if (date.isValid()) {
                        value = date.format("hh:mm:ss a");
                    }
                    return value;
                },
                width: 120
            },
            {
                headerName: "Out Time", field: "checkOut", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 2) {
                        return '-';
                    }
                    let value = params.value;
                    let date = moment(value, "hh:mm:ss a");
                    if (date.isValid()) {
                        value = date.format("hh:mm:ss a");
                    }
                    return value;
                },
                width: 150
            },
            {
                headerName: "Production / Day", field: "production", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 1) {
                        return '-';
                    }
                    let value = params.value;
                    return value + ' / ' + params.data.requiredProduction;
                },
                width: 160
            },

            { headerName: "Hours", field: "overtimeHours", sortable: true, width: 160},
            { headerName: "Status", field: "statusName", sortable: true, width: 160},
            { headerName: "Comments", field: "reason", sortable: true, width: 330 },
            {
                headerName: "Action",
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

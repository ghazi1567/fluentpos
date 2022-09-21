import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { RequestStatusMapping, RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { AttendanceService } from "../../services/attendance.service";
import { OvertimeService } from "../../services/overtime.service";

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
    overtimeColumns: TableColumn[];
    overtimeParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;
    actionButtons: CustomAction[] = [new CustomAction("Approve", "approved", "Update", "check"), new CustomAction("Reject", "rejected", "Update", "close", "warn")];
    attendanceRequestPermission = ["Permissions.AttendanceRequests.MyQueue"];
    overtimeRequestPermission = ["Permissions.OvertimeRequests.MyQueue"];
    constructor(public attendanceService: AttendanceService, public overtimeService: OvertimeService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}

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
        this.overtimeService.getMyQueue(this.overtimeParams).subscribe((result) => {
            this.overtimes = result;
            this.overtimes.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
        });
    }

    initAttendanceColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: [""] }
        ];
    }
    initOvertimeColumns(): void {
        this.overtimeColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Overtime Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: [""] }
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
                message: `Do you confirm the removal of this ${data.button.key}?`,
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
                if (result.rowData.requestType == RequestType.Attendance) {
                    this.updateAttendanceApproval(result);
                }
                if (result.rowData.requestType == RequestType.OverTime) {
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
}

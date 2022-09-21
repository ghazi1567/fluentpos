import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { RequestStatus, RequestStatusMapping } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { AttendanceService } from "../../services/attendance.service";
import { AttendanceFormComponent } from "./attendance-form/attendance-form.component";

@Component({
    selector: "app-attendance",
    templateUrl: "./attendance.component.html",
    styleUrls: ["./attendance.component.scss"]
})
export class AttendanceComponent implements OnInit {
    attendances: PaginatedResult<EmployeeRequest>;
    attendanceColumns: TableColumn[];
    attendanceParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;
    constructor(public attendanceService: AttendanceService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}

    ngOnInit(): void {
        this.getAttendances();
        this.initColumns();
    }

    getAttendances(): void {
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceService.getAll(this.attendanceParams).subscribe((result) => {
            this.attendances = result;
            this.attendances.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.Pending || x.status == RequestStatus.Rejected;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
        });
    }

    initColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true  },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register","Update", "Remove"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.attendanceParams.pageNumber = event.pageNumber;
        this.attendanceParams.pageSize = event.pageSize;
        this.getAttendances();
    }

    openForm(customer?: EmployeeRequest): void {
        const dialogRef = this.dialog.open(AttendanceFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAttendances();
            }
        });
    }

    remove($event: string): void {
        this.attendanceService.delete($event).subscribe(() => {
            this.getAttendances();
            this.toastr.info("Attendances Removed");
        });
    }

    sort($event: Sort): void {
        this.attendanceParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.attendanceParams.orderBy);
        this.getAttendances();
    }

    filter($event: string): void {
        this.attendanceParams.searchString = $event.trim().toLocaleLowerCase();
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendances();
    }

    reload(): void {
        this.attendanceParams.searchString = "";
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendances();
    }
}

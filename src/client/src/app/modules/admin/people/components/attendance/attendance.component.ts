import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { AttendanceStatus, AttendanceStatusMapping } from "src/app/core/enums/AttendanceStatus";
import { AttendanceType, AttendanceTypeMapping } from "src/app/core/enums/AttendanceType";
import { RequestStatus, RequestStatusMapping } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { Attendance } from "../../models/Attendance";
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
    attendances: PaginatedResult<Attendance>;
    attendanceColumns: TableColumn[];
    attendanceParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;
    public AttendanceStatusMapping = AttendanceStatusMapping;
    public AttendanceTypeMapping = AttendanceTypeMapping;
    
    public attendanceStatus = Object.values(AttendanceStatus).filter((value) => typeof value === "number");
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
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.attendanceTypeName = AttendanceTypeMapping[x.attendanceType];
                x.className = this.AttendanceClass(x.attendanceStatus);
                // x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                // x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.Pending || x.status == RequestStatus.Rejected;
                // x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
        });
       
    }

    AttendanceClass(status: AttendanceStatus){
        var className = ''
        switch (status) {
            case AttendanceStatus.Absent:
                className ='cls-absent'
                break;
            case AttendanceStatus.Off:
                className ='cls-off'
                break;
            case AttendanceStatus.Present:
                className ='cls-present'
                break;
            case AttendanceStatus.Holiday:
                className ='cls-holiday'
                break;
            case AttendanceStatus.Leave:
                className ='cls-leave'
                break;
        
            default:
                break;
        }
        return className;
    }
    initColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true  },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "attendanceStatusName", isSortable: true, isShowable: true, isClass:true },
            { name: "Earned Hours", dataKey: "totalEarnedHours", isSortable: true, isShowable: true },
            { name: "Type", dataKey: "attendanceTypeName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Update"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.attendanceParams.pageNumber = event.pageNumber;
        this.attendanceParams.pageSize = event.pageSize;
        this.getAttendances();
    }

    openForm(customer?: Attendance): void {
        if(customer && customer.attendanceType == AttendanceType.OverTime){
            return;
        }
        const dialogRef = this.dialog.open(AttendanceFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getAttendances();
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

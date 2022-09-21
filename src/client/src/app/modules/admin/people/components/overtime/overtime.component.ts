import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { RequestStatusMapping, RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { OvertimeService } from "../../services/overtime.service";
import { OvertimeFormComponent } from "./overtime-form/overtime-form.component";

@Component({
    selector: "app-overtime",
    templateUrl: "./overtime.component.html",
    styleUrls: ["./overtime.component.scss"]
})
export class OvertimeComponent implements OnInit {
    overtimes: PaginatedResult<EmployeeRequest>;
    overtimesColumns: TableColumn[];
    overtimesParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;
    constructor(public overtimeService: OvertimeService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) {}

    ngOnInit(): void {
        this.getAttendances();
        this.initColumns();
    }

    getAttendances(): void {
        this.overtimesParams.employeeId = this.authService.getEmployeeId;
        this.overtimesParams.requestType = RequestType.OverTime;
        this.overtimeService.getAll(this.overtimesParams).subscribe((result) => {
            this.overtimes = result;
            this.overtimes.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.Pending || x.status == RequestStatus.Rejected;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
        });
    }

    initColumns(): void {
        this.overtimesColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Ovetime Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register", "Update", "Remove"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.overtimesParams.pageNumber = event.pageNumber;
        this.overtimesParams.pageSize = event.pageSize;
        this.getAttendances();
    }

    openForm(customer?: EmployeeRequest): void {
        const dialogRef = this.dialog.open(OvertimeFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAttendances();
            }
        });
    }

    remove($event: string): void {
        this.overtimeService.delete($event).subscribe(() => {
            this.getAttendances();
            this.toastr.info("Overtime Removed");
        });
    }

    sort($event: Sort): void {
        this.overtimesParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.overtimesParams.orderBy);
        this.getAttendances();
    }

    filter($event: string): void {
        this.overtimesParams.searchString = $event.trim().toLocaleLowerCase();
        this.overtimesParams.pageNumber = 0;
        this.overtimesParams.pageSize = 0;
        this.getAttendances();
    }

    reload(): void {
        this.overtimesParams.searchString = "";
        this.overtimesParams.pageNumber = 0;
        this.overtimesParams.pageSize = 0;
        this.getAttendances();
    }
}

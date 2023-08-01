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
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { OvertimeService } from "../../services/overtime.service";
import { OvertimeFormComponent } from "./overtime-form/overtime-form.component";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import * as moment from "moment";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";

@Component({
    selector: "app-overtime",
    templateUrl: "./overtime.component.html",
    styleUrls: ["./overtime.component.scss"]
})
export class OvertimeComponent implements OnInit {
    overtimes: PaginatedResult<EmployeeRequest>;
    overtimesData: EmployeeRequest[] = [];
    overtimesColumns: TableColumn[];
    overtimesParams = new PeopleSearchParams();
    searchString: string;
    public RequestStatusMapping = RequestStatusMapping;

    overtimeColumns: any[];


    constructor(public overtimeService: OvertimeService, public dialog: MatDialog, public toastr: ToastrService, public authService: AuthService) { }

    ngOnInit(): void {
        this.getAttendances();
        this.initColumns();
        this.initColumns1();
    }

    getAttendances(): void {
        this.overtimesParams.employeeId = this.authService.getEmployeeId;
        this.overtimesParams.requestType = RequestType.OverTime;
        this.overtimesParams.pageNumber = 0;
        this.overtimesParams.pageSize = 100000;
        this.overtimeService.getAll(this.overtimesParams).subscribe((result) => {
            this.overtimes = result;

            this.overtimes.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.View = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
                x.Update = x.status == RequestStatus.Approved || x.status == RequestStatus.Pending || x.status == RequestStatus.Rejected;
                x.Remove = x.status == RequestStatus.Approved || x.status == RequestStatus.InProgress;
            });
            this.overtimesData = this.overtimes.data
        });
    }

    initColumns1(): void {
        this.overtimesColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Ovetime Type", dataKey: "overTimeType", isSortable: true, isShowable: true },
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
        this.overtimesParams.pageSize = 100000;
        this.getAttendances();
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
        this.getAttendances();
    }
    actionButtons: CustomAction[] = [new CustomAction("Remove", "Remove", "Remove", "delete")];
    initColumns(): void {
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

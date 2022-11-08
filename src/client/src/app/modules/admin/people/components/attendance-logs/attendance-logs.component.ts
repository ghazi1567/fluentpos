import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { RequestStatus, RequestStatusMapping } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { BioAttendance } from "../../models/bioAttendance";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { AttendanceRequestService } from "../../services/attendance-request.service";
import { AttendanceLogFormComponent } from "./attendance-log-form/attendance-log-form.component";

@Component({
    selector: "app-attendance-logs",
    templateUrl: "./attendance-logs.component.html",
    styleUrls: ["./attendance-logs.component.scss"]
})
export class AttendanceLogsComponent implements OnInit {
    attendances: PaginatedResult<EmployeeRequest>;
    bioAttendances: PaginatedResult<BioAttendance>;
    attendanceColumns: TableColumn[];
    bioAttendanceColumns: TableColumn[];
    attendanceParams = new PeopleSearchParams();
    searchString: string;
    @ViewChild("file") fileInput: ElementRef;
    public RequestStatusMapping = RequestStatusMapping;
    customActionData: CustomAction=  new CustomAction('Save','save','register','save');
    constructor(
        public attendanceRequestService: AttendanceRequestService,
        private csvParser: CsvParserService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.getAttendances();
        this.initColumns();
        this.initBioColumns();
        this.bioAttendances = <PaginatedResult<BioAttendance>>{
            currentPage: 0,
            data: [],
            pageSize: 5,
            totalCount: 0,
            totalPages: 0
        };
    }

    getAttendances(): void {
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceRequestService.getAll(this.attendanceParams).subscribe((result) => {
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
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register", "Update", "Remove"] }
        ];
    }
    initBioColumns(): void {
        this.bioAttendanceColumns = [
            { name: "Person Id", dataKey: "personid", isSortable: true, isShowable: true },
            { name: "Employee Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "department", dataKey: "department", isSortable: true, isShowable: true },
            { name: "attendanceDateTime", dataKey: "time", isSortable: true, isShowable: true, columnType: "date", format: "short" },
            { name: "attendanceStatus", dataKey: "attendanceStatus", isSortable: true, isShowable: true },
            { name: "customName", dataKey: "customName", isSortable: true, isShowable: true },
            { name: "attendanceCheckPoint", dataKey: "attendanceCheckPoint", isSortable: true, isShowable: false },
            { name: "dataSource", dataKey: "dataSource", isSortable: true, isShowable: false },
            { name: "handlingType", dataKey: "handlingType", isSortable: true, isShowable: false },
            { name: "temperature", dataKey: "temperature", isSortable: true, isShowable: false },
            { name: "abnormal", dataKey: "abnormal", isSortable: true, isShowable: false },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register"] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.attendanceParams.pageNumber = event.pageNumber;
        this.attendanceParams.pageSize = event.pageSize;
        this.getAttendances();
    }

    openForm(customer?: EmployeeRequest): void {
        const dialogRef = this.dialog.open(AttendanceLogFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getAttendances();
            }
        });
    }

    remove($event: string): void {
        this.attendanceRequestService.delete($event).subscribe(() => {
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

    handleFileSelect(evt) {
        var files = evt.target.files;
        console.log(files);
        this.csvParser
            .parse(files[0], {
                header: true,
                delimiter: ",",
                mapping: this.csvMapping
            })
            .pipe()
            .subscribe(
                (result: Array<BioAttendance>) => {
                    console.log(result);
                    this.bioAttendances = <PaginatedResult<BioAttendance>>{
                        currentPage: 0,
                        data: result,
                        pageSize: 5,
                        totalCount: result.length,
                        totalPages: result.length / 10
                    };
                },
                (error: NgxCSVParserError) => {
                    console.log("Error", error);
                }
            );
    }
    openFormBioMatric() {
        this.fileInput.nativeElement.click();
    }
    csvMapping: CsvMapping[];
    initCsvMapping() {
        this.csvMapping = [
            {
                csvColumn: "personid",
                gridColumn: "personid"
            },
            {
                csvColumn: "name",
                gridColumn: "name"
            },
            {
                csvColumn: "department",
                gridColumn: "department"
            },
            {
                csvColumn: "time",
                gridColumn: "time"
            },
            {
                csvColumn: "attendanceStatus",
                gridColumn: "attendanceStatus"
            },
            {
                csvColumn: "customName",
                gridColumn: "customName"
            },
            {
                csvColumn: "attendanceCheckPoint",
                gridColumn: "attendanceCheckPoint"
            },
            {
                csvColumn: "dataSource",
                gridColumn: "dataSource"
            },
            {
                csvColumn: "handlingType",
                gridColumn: "handlingType"
            },
            {
                csvColumn: "temperature",
                gridColumn: "temperature"
            },
            {
                csvColumn: "abnormal",
                gridColumn: "abnormal"
            }
        ];
    }

    onImportFile($event){
        console.log($event);
    }
}

import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { IServerSideDatasource } from "ag-grid-community";
import { ToastrService } from "ngx-toastr";
import { RequestStatus, RequestStatusMapping } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { NgAsConfig, NgAsSearchTerm } from "src/app/core/models/Filters/SearchTerm";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { BioAttendance } from "../../models/bioAttendance";
import { EmployeeRequest } from "../../models/employeeRequest";
import { PeopleSearchParams } from "../../models/peopleSearchParams";
import { AttendanceLogService } from "../../services/attendance-log.service";
import { AttendanceRequestService } from "../../services/attendance-request.service";
import { AttendanceLogFormComponent } from "./attendance-log-form/attendance-log-form.component";
import * as moment from "moment";
@Component({
    selector: "app-attendance-logs",
    templateUrl: "./attendance-logs.component.html",
    styleUrls: ["./attendance-logs.component.scss"]
})
export class AttendanceLogsComponent implements OnInit {
    attendances: PaginatedResult<EmployeeRequest>;
    bioAttendances: PaginatedResult<BioAttendance>;
    bioAttendancesData: BioAttendance[] = [];
    importAttendances: PaginatedResult<BioAttendance>;
    attendanceColumns: TableColumn[];
    bioAttendanceColumns: any[];
    importAttendanceColumns: TableColumn[];
    attendanceParams = new PeopleSearchParams();
    attendanceLogsParams = new PeopleSearchParams();
    searchString: string;
    @ViewChild("file") fileInput: ElementRef;
    public RequestStatusMapping = RequestStatusMapping;
    customActionData: CustomAction = new CustomAction("Save", "save", "register", "save");
    advanceSearch: NgAsConfig;
    savedFilters: NgAsSearchTerm[];
    isAdvanceFilter = false;
    constructor(
        public attendanceRequestService: AttendanceRequestService,
        public attendanceLogService: AttendanceLogService,
        private csvParser: CsvParserService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.attendanceLogsParams.startDate = new Date();
        this.attendanceLogsParams.endDate = new Date();
        this.getAttendances();
        // this.getAttendancesLogs();
        this.initColumns();
        this.initBioColumns();
        this.initAdvanceFilters();
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
    getAttendancesLogs(params = null): void {
        console.log(params);
        this.attendanceLogsParams.employeeId = this.authService.getEmployeeId;
        this.attendanceLogsParams.requestType = RequestType.Attendance;
        this.attendanceLogsParams.pageSize = 100000;
        this.attendanceLogService.advanceSearch(this.attendanceLogsParams).subscribe((result) => {
            this.bioAttendances = result;
            this.bioAttendancesData = result.data;
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
        this.handleSearch();
    }

    initColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "requestedForName", isSortable: true, isShowable: true },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true, columnType: "date", format: "dd-MM-yy hh:mm a" },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true, columnType: "date", format: "dd-MM-yy hh:mm a" },
            { name: "Status", dataKey: "statusName", isSortable: true, isShowable: true },
            { name: "Comments", dataKey: "reason", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Register", "Update", "Remove"] }
        ];
    }
    initBioColumns(): void {
        this.bioAttendanceColumns = [
            { headerName: "Punch Code", field: "punchCode", sortable: true },
            { headerName: "Employee Name", field: "name", sortable: true },
            { headerName: "Card No", field: "cardNo", sortable: true },
            {
                headerName: "Att. DateTime",
                field: "attendanceDateTime",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD hh:mm:ss A");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD hh:mm:ss A");
                    }
                    return value;
                }
            },
            {
                headerName: "Att. Date",
                field: "attendanceDate",
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
                headerName: "Att. Time",
                field: "attendanceTime",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    if (value) {
                        value = value.split("T");
                    }
                    return value.length > 1 ? value[1] : "";
                }
            },
            { headerName: "Direction", field: "direction", sortable: true },
            { headerName: "Device Serial No", field: "deviceSerialNo", sortable: true },
            { headerName: "Device Name", field: "deviceName", sortable: true }
        ];
        // this.bioAttendanceColumns = [
        //     { name: "Punch Code", dataKey: "punchCode", isSortable: true, isShowable: true },
        //     { name: "Employee Name", dataKey: "name", isSortable: true, isShowable: true },
        //     { name: "Card No", dataKey: "cardNo", isSortable: true, isShowable: true },
        //     { name: "Att. DateTime", dataKey: "attendanceDateTime", isSortable: true, isShowable: true, columnType: "date", format: "short" },
        //     { name: "Att. Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "shortDate" },
        //     { name: "Att. Time", dataKey: "attendanceTime", isSortable: true, isShowable: true, columnType: "date", format: "shortTime" },
        //     { name: "Direction", dataKey: "direction", isSortable: true, isShowable: true },
        //     { name: "Device Serial No", dataKey: "deviceSerialNo", isSortable: true, isShowable: false },
        //     { name: "Device Name", dataKey: "deviceName", isSortable: true, isShowable: false },
        //     { name: "Action", dataKey: "action", position: "right", buttons: [""] }
        // ];
    }
    initImportColumns(): void {
        this.importAttendanceColumns = [
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
    logsPageChanged(event: PaginatedFilter): void {
        this.attendanceLogsParams.pageNumber = event.pageNumber;
        this.attendanceLogsParams.pageSize = event.pageSize;
        this.getAttendancesLogs();
    }

    openForm(customer?: EmployeeRequest): void {
        const dialogRef = this.dialog.open(AttendanceLogFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getAttendances();
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
    logsSort($event: Sort): void {
        this.attendanceLogsParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.attendanceLogsParams.orderBy);
        this.getAttendancesLogs();
    }

    filter($event: string): void {
        this.attendanceParams.searchString = $event.trim().toLocaleLowerCase();
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendances();
    }
    logsFilter($event: string): void {
        this.attendanceLogsParams.searchString = $event.trim().toLocaleLowerCase();
        this.attendanceLogsParams.pageNumber = 0;
        this.attendanceLogsParams.pageSize = 0;
        this.getAttendancesLogs();
    }

    reload(): void {
        this.attendanceParams.searchString = "";
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        this.getAttendances();
    }
    logsReload(): void {
        this.attendanceLogsParams.searchString = "";
        this.attendanceLogsParams.pageNumber = 0;
        this.attendanceLogsParams.pageSize = 0;
        this.getAttendancesLogs();
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

    onImportFile($event) {
        console.log($event);
    }
    onAdvanceFilters($event) {
        this.attendanceLogsParams.advanceFilters = $event.advancedTerms;
        this.attendanceLogsParams.advancedSearchType = $event.advancedSearchType;
        this.isAdvanceFilter = true;
        this.getAttendancesLogs();
    }

    initAdvanceFilters() {
        this.savedFilters = [];
        this.advanceSearch = {
            headers: [
                { id: "name", displayText: "Employee Name" },
                { id: "cardNo", displayText: "Card#" },
                { id: "PunchCode", displayText: "Punch Code", type: "number" },
                {
                    id: "attendanceDate",
                    displayText: "Attendance Date",
                    type: "date"
                }
            ],
            defaultTerm: null,
            inputArray: null,
            savedFilters: this.savedFilters,
            showFilterSaving: null,
            simpleFieldLabel: null
        };
    }

    handleSearch() {
        this.attendanceLogsParams.advanceFilters = [];
        this.attendanceLogsParams.advanceFilters.push({
            fieldName: "attendanceDate",
            searchTerm: this.attendanceLogService.getDateStringOnly(this.attendanceLogsParams.startDate),
            action: ">="
        });
        this.attendanceLogsParams.advanceFilters.push({
            fieldName: "attendanceDate",
            searchTerm: this.attendanceLogService.getDateStringOnly(this.attendanceLogsParams.endDate),
            action: "<="
        });
        this.attendanceLogsParams.advancedSearchType = "and";
        this.getAttendancesLogs();
    }
}

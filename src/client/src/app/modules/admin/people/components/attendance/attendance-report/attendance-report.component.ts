import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ColDef, IGetRowsParams, ValueFormatterParams } from "ag-grid-community";
import * as moment from "moment";
import { ToastrService } from "ngx-toastr";
import { AttendanceStatusMapping, AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
import { AttendanceTypeMapping, AttendanceType } from "src/app/core/enums/AttendanceType";
import { RequestStatusMapping } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { NgAsConfig } from "src/app/core/models/Filters/SearchTerm";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CsvParserService } from "src/app/core/services/csv-parser.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Attendance } from "../../../models/Attendance";
import { Employee } from "../../../models/employee";
import { PeopleSearchParams } from "../../../models/peopleSearchParams";
import { AttendanceService } from "../../../services/attendance.service";
import { EmployeeService } from "../../../services/employee.service";
import { AttendanceFormComponent } from "../attendance-form/attendance-form.component";

@Component({
    selector: "app-attendance-report",
    templateUrl: "./attendance-report.component.html",
    styleUrls: ["./attendance-report.component.scss"]
})
export class AttendanceReportComponent implements OnInit {
    title = "Attendance Report";
    subtitle = "";
    attendances: PaginatedResult<Attendance>;
    attendanceRows: Attendance[];
    attendanceColumns: any[];
    attendanceParams: PeopleSearchParams;
    searchString: string;
    employeesLookup: Employee[];
    public RequestStatusMapping = RequestStatusMapping;
    public AttendanceStatusMapping = AttendanceStatusMapping;
    public AttendanceTypeMapping = AttendanceTypeMapping;
    advanceSearch: NgAsConfig;
    public attendanceStatus = Object.values(AttendanceStatus).filter((value) => typeof value === "number");
    constructor(
        public attendanceService: AttendanceService,
        private employeeService: EmployeeService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public authService: AuthService,
        private csvParser: CsvParserService
    ) {}

    ngOnInit(): void {
        this.attendanceParams = new PeopleSearchParams();
        this.attendanceParams.startDate = new Date();
        this.attendanceParams.endDate = new Date();
        this.attendanceRows = [];
        this.loadLookups();
        // this.getAttendances();
        this.initColumns();
    }
    loadLookups() {
        let employeeParams = new SearchParams();
        this.employeeService.getEmployees(employeeParams).subscribe((res) => {
            this.employeesLookup = res.data;
            this.initAdvanceFilters();
        });
    }
    initAdvanceFilters() {
        var employeesLookupData = this.employeesLookup.map((emp) => ({
            key: emp.id,
            value: emp.fullName
        }));
        this.advanceSearch = {
            headers: [
                { id: "EmployeeId", displayText: "Epmployee", type: "dropdown", data: employeesLookupData },
                { id: "ApprovedBy", displayText: "Approved By", type: "dropdown", data: employeesLookupData },
                // { id: "DepartmentId", displayText: "Attendance Date", type: "date" },
                { id: "AttendanceDate", displayText: "Attendance Date", type: "date" },
                {
                    id: "AttendanceStatus",
                    displayText: "Attendance Status",
                    type: "dropdown",
                    data: [
                        {
                            key: "0",
                            value: "None"
                        },
                        {
                            key: "1",
                            value: "Present"
                        },
                        {
                            key: "2",
                            value: "Absent"
                        },
                        {
                            key: "3",
                            value: "Leave"
                        },
                        {
                            key: "4",
                            value: "Holiday"
                        },
                        {
                            key: "5",
                            value: "Off"
                        }
                    ]
                },
                {
                    id: "AttendanceType",
                    displayText: "Attendance Type",
                    type: "dropdown",
                    data: [
                        {
                            key: "0",
                            value: "Bio"
                        },
                        {
                            key: "1",
                            value: "Manual"
                        },
                        {
                            key: "2",
                            value: "OverTime"
                        },
                        {
                            key: "3",
                            value: "System"
                        }
                    ]
                },
                { id: "CheckIn", displayText: "Check In", type: "time" },
                { id: "CheckOut", displayText: "Check Out", type: "time" },
                { id: "ExpectedIn", displayText: "Expected Check In", type: "time" },
                { id: "ExpectedOut", displayText: "Expected Check Out", type: "time" },
                {
                    id: "Status",
                    displayText: "Request Status",
                    type: "dropdown",
                    data: [
                        {
                            key: "0",
                            value: "Pending"
                        },
                        {
                            key: "1",
                            value: "Rejected"
                        },
                        {
                            key: "2",
                            value: "Approved"
                        },
                        {
                            key: "3",
                            value: "InProgress"
                        }
                    ]
                },
                {
                    id: "OverTimeType",
                    displayText: "OverTime Type",
                    type: "dropdown",
                    data: [
                        {
                            key: "0",
                            value: "None"
                        },
                        {
                            key: "1",
                            value: "Hours"
                        },
                        {
                            key: "2",
                            value: "Production"
                        }
                    ]
                },
                {
                    id: "Production",
                    displayText: "OverTime Production Hrs",
                    type: "number"
                },
                {
                    id: "EarnedHours",
                    displayText: "Atten. Hours",
                    type: "number"
                },
                {
                    id: "OvertimeHours",
                    displayText: "Overtime Hours",
                    type: "number"
                }
            ],
            defaultTerm: null,
            inputArray: null,
            savedFilters: [],
            showFilterSaving: null,
            simpleFieldLabel: null
        };
    }

    handleSearch() {
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceService.attendanceReport(this.attendanceParams).subscribe((result) => {
            this.attendances = result;
            this.attendances.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.attendanceTypeName = AttendanceTypeMapping[x.attendanceType];
                x.className = this.AttendanceClass(x.attendanceStatus);
            });

            this.attendanceRows = this.attendances.data;

            //this.AgGrid.gridApi.setRowData(this.attendanceRows);
        });
    }
    totalRecords;
    getAttendances(params): void {
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceService.attendanceReport(this.attendanceParams).subscribe((result) => {
            this.attendances = result;
            this.attendances.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.attendanceTypeName = AttendanceTypeMapping[x.attendanceType];
                x.className = this.AttendanceClass(x.attendanceStatus);
            });
            this.totalPages = result.totalPages;
            this.totalRecords = result.totalCount;
            this.setData(params, this.attendances);
        });
    }
    currentPageNo;
    runningTotal;
    setData(params, resp) {
        this.AgGrid.gridApi.hideOverlay();
        if (resp && resp.data && resp.data.items && resp.data.items.length > 0) {
            this.runningTotal = params.startRow + resp.data.items.length;
            // params.successCallback(resp.data.items);
        } else {
            // params.successCallback([]);
            this.AgGrid.gridApi.showNoRowsOverlay();
        }
    }

    AttendanceClass(status: AttendanceStatus) {
        var className = "";
        switch (status) {
            case AttendanceStatus.Absent:
                className = "cls-absent";
                break;
            case AttendanceStatus.Off:
                className = "cls-off";
                break;
            case AttendanceStatus.Present:
                className = "cls-present";
                break;
            case AttendanceStatus.Holiday:
                className = "cls-holiday";
                break;
            case AttendanceStatus.Leave:
                className = "cls-leave";
                break;

            default:
                break;
        }
        return className;
    }
    scrollBarDataSource: {
        getRows: (params: IGetRowsParams) => void;
    };
    pageSize = 100;
    totalPages;
    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    submissionQuery;
    gridReady(event): void {
        console.log("on grid ready", event);
        // this.scrollBarDataSource = {
        //     getRows: (params: IGetRowsParams) => {
        //         // if ((this.totalPages > 0 && this.currentPageNo > this.totalPages) || this.totalPages <=0) {
        //         //     params.successCallback([]);
        //         //     this.TRCSubmisGrid.gridAPI.showNoRowsOverlay();
        //         //     this.TRCSubmisGrid.gridAPI.setInfiniteRowCount(0, true); // remove empty row
        //         // } else {
        //         this.AgGrid.gridApi.showLoadingOverlay();
        //         console.log("on grid ready", params);
        //         console.log("this.totalPages", this.totalPages);
        //         this.submissionQuery = {
        //             // filters: this.submissionFilter,
        //             currentPage: params.startRow / this.pageSize, // this.currentPageNo,
        //             pageSize: this.pageSize,
        //             sortBy: params && params.sortModel && params.sortModel.length > 0 ? params.sortModel[0].colId : "",
        //             sortOrder: params && params.sortModel && params.sortModel.length > 0 ? params.sortModel[0].sort : ""
        //         };
        //         this.getAttendances(params);

        //         //  }
        //     }
        // };
        if (this.AgGrid) {
            // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
        }
        this.getAttendances(event.params);
    }
    initColumns(): void {
        const cellClassRules = {
            "cell-pass": (params) => params.value > 0,
            "cell-fail": (params) => params.value < 0
        };
        this.attendanceColumns = [
            { headerName: "Department Name", field: "departmentName", sortable: true, rowGroup: true, enableRowGroup: true, cellRenderer: "agGroupCellRenderer" },
            { headerName: "Employee Name", field: "employeeName", sortable: true, rowGroup: true, enableRowGroup: true, cellRenderer: "agGroupCellRenderer" },
            {
                headerName: "Attendance Date",
                field: "attendanceDate",
                rowGroup: true,
                enableRowGroup: true,
                sortable: true,
                pivot: true,
                valueGetter: (params) => {
                    if (!params.node.group) {
                        let value = params.data.attendanceDate;
                        let date = moment(value, "YYYY-MM-DD");
                        if (date.isValid()) {
                            value = date.format("YYYY-MM-DD");
                        }
                        // no need to handle group levels - calculated in the 'ratioAggFunc'
                        return value;
                    }
                },
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
                headerName: "Status",
                field: "attendanceStatusName",
                sortable: true,
                isShowable: true,
                width: 250,
                cellClassRules: {
                    "bg-green": "x == 'Present'",
                    "bg-red": "x =='Absent'"
                },
                aggFunc: (params) => {
                    var values = params.values.filter((x) => x.trim() != "");
                    if (values.length == 1) {
                        return values[0];
                    } else {
                        const groupByStatus = values.reduce((group, product) => {
                            group[product] = group[product] ?? 0;
                            group[product] = group[product] + 1;
                            return group;
                        }, {});

                        var str = "";
                        for (var key in groupByStatus) {
                            str = str + ` ${key} = ${groupByStatus[key]}`;
                        }
                        return str;
                    }
                }
            },
            {
                headerName: "Check In",
                field: "checkIn",
                sortable: true,
                isShowable: true,
                width: 150
            },
            {
                headerName: "Check Out",
                field: "checkOut",
                sortable: true,
                isShowable: true,
                width: 150
            },
            {
                headerName: "Late Comer",
                field: "isLateComer",
                sortable: true,
                isShowable: true,
                width: 150,
                valueGetter: (params) => {
                    if (!params.node.group) {
                        if (params.data.isLateComer == true) {
                            return "Yes";
                        }
                        return "No";
                    }
                }
            },
            {
                headerName: "Late Minutes",
                field: "lateMinutes",
                sortable: true,
                isShowable: true,
                valueFormatter: numberFormatter,
                width: 150
            },
            {
                headerName: "Earned Hours",
                field: "earnedHours",
                sortable: true,
                isShowable: true,
                valueFormatter: numberFormatter,
                width: 150
            },
            {
                headerName: "Overtime Hours",
                field: "overtimeHours",
                sortable: true,
                valueFormatter: numberFormatter,
                width: 150
            },
            {
                headerName: "Actual Earned Hours",
                field: "actualEarnedHours",
                sortable: true,
                valueFormatter: numberFormatter,
                width: 150
            }
        ];
    }
    public autoGroupColumnDef: ColDef = {
        headerName: "Employee Name",
        field: "employeeName"
    };

    pageChanged(event: PaginatedFilter): void {
        this.attendanceParams.pageNumber = event.pageNumber;
        this.attendanceParams.pageSize = event.pageSize;
    }

    openForm(customer?: Attendance): void {
        if (customer && customer.attendanceType == AttendanceType.OverTime) {
            return;
        }
        const dialogRef = this.dialog.open(AttendanceFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            // this.getAttendances();
        });
    }

    remove($event: string): void {
        this.attendanceService.delete($event).subscribe(() => {
            // this.getAttendances();
            this.toastr.info("Attendances Removed");
        });
    }

    sort($event: Sort): void {
        var col = $event.active;
        switch (col) {
            case "statusName":
                col = "status";
                break;
            case "attendanceStatusName":
                col = "attendanceStatus";
                break;
            case "attendanceTypeName":
                col = "attendanceType";
                break;
            default:
                break;
        }

        this.attendanceParams.orderBy = col + " " + $event.direction;
        // this.getAttendances();
    }

    filter($event: string): void {
        this.attendanceParams.searchString = $event.trim().toLocaleLowerCase();
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        // this.getAttendances();
    }

    reload(): void {
        this.attendanceParams.searchString = "";
        this.attendanceParams.pageNumber = 0;
        this.attendanceParams.pageSize = 0;
        // this.getAttendances();
    }

    onAdvanceFilters($event) {
        if ($event == null) {
            this.attendanceParams.advanceFilters = null;
            this.attendanceParams.advancedSearchType = null;
        } else {
            this.attendanceParams.advanceFilters = $event.advancedTerms;
            this.attendanceParams.advancedSearchType = $event.advancedSearchType;
        }

        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;
        // this.getAttendances();
    }

    onExportFile($event) {
        var parms = this.attendanceParams;
        parms.pageNumber = 0;
        parms.pageSize = 100000;

        this.attendanceService.advanceSearch(parms).subscribe((result) => {
            var attendances = [];
            result.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.attendanceTypeName = AttendanceTypeMapping[x.attendanceType];
                x.className = this.AttendanceClass(x.attendanceStatus);

                attendances.push({
                    employeeName: x.employeeName,
                    attendanceDate: new Date(x.attendanceDate).toLocaleDateString(),
                    attendanceType: x.attendanceTypeName,
                    attendanceStatus: x.attendanceStatusName,
                    checkIn: x.checkIn,
                    checkOut: x.checkOut,
                    isCheckOutMissing: x.isCheckOutMissing,
                    isLateComer: x.isLateComer,
                    overtimeHours: x.overtimeHours,
                    earnedHours: x.earnedHours,
                    punchCode: x.punchCode
                });
            });
            this.csvParser.exportXls(attendances, "Attendance.xlsx", "Attendance");
        });
    }
}
function numberFormatter(params: ValueFormatterParams): string {
    if (!params.value || params.value === 0) return "0";
    return "" + Math.round(params.value * 100) / 100;
}

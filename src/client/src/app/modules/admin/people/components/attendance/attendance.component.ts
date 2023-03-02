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
import { NgAsConfig, NgAsSearchTerm } from "src/app/core/models/Filters/SearchTerm";
import { EmployeeService } from "../../services/employee.service";
import { Employee } from "../../models/employee";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";

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
        this.loadLookups();
        this.getAttendances();
        this.initColumns();
    }
    loadLookups() {
        let employeeParams = new SearchParams();
        employeeParams.pageSize = 10000;
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
    getAttendances(): void {
        this.attendanceParams.employeeId = this.authService.getEmployeeId;
        this.attendanceParams.requestType = RequestType.Attendance;

        this.attendanceService.advanceSearch(this.attendanceParams).subscribe((result) => {
            this.attendances = result;
            this.attendances.data.forEach((x) => {
                x.statusName = RequestStatusMapping[x.status];
                x.attendanceStatusName = AttendanceStatusMapping[x.attendanceStatus];
                x.attendanceTypeName = AttendanceTypeMapping[x.attendanceType];
                x.className = this.AttendanceClass(x.attendanceStatus);
            });
        });
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
    initColumns(): void {
        this.attendanceColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Employee Name", dataKey: "employeeName", isSortable: true, isShowable: true },
            { name: "Attendance Date", dataKey: "attendanceDate", isSortable: true, isShowable: true, columnType: "date", format: "dd MMM yyyy" },
            { name: "In Time", dataKey: "checkIn", isSortable: true, isShowable: true },
            { name: "Out Time", dataKey: "checkOut", isSortable: true, isShowable: true },
            { name: "Actual Earned Hours", dataKey: "actualEarnedHours", isSortable: true, isShowable: true },
            { name: "Earned Hours", dataKey: "earnedHours", isSortable: true, isShowable: true },
            { name: "Overtime Hours", dataKey: "overtimeHours", isSortable: true, isShowable: true },
            { name: "Status", dataKey: "attendanceStatusName", isSortable: true, isShowable: true, isClass: true },
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
        if (customer && customer.attendanceType == AttendanceType.OverTime) {
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
        var col = $event.active;
        switch (col) {
            case "statusName":
                col = "status"
                break;
            case "attendanceStatusName":
                col = "attendanceStatus"
                break;
            case "attendanceTypeName":
                col = "attendanceType"
                break;
            default:
                break;
        }

        this.attendanceParams.orderBy = col + " " + $event.direction;
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
        this.getAttendances();
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

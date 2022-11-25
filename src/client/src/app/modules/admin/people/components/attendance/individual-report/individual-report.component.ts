import { Component, OnInit } from "@angular/core";
import { AttendanceStatus, AttendanceStatusMapping } from "src/app/core/enums/AttendanceStatus";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Attendance } from "../../../models/Attendance";
import { Employee } from "../../../models/employee";
import { PeopleSearchParams } from "../../../models/peopleSearchParams";
import { AttendanceService } from "../../../services/attendance.service";
import { EmployeeService } from "../../../services/employee.service";
import { SearchParams } from "src/app/modules/admin/org/models/SearchParams";
import { ToastrService } from "ngx-toastr";
import { MatDialog } from "@angular/material/dialog";
import { AttendanceFormComponent } from "../attendance-form/attendance-form.component";
import { AttendanceType } from "src/app/core/enums/AttendanceType";
import { AttendanceRequestService } from "../../../services/attendance-request.service";
import { AttendanceLogFormComponent } from "../../attendance-logs/attendance-log-form/attendance-log-form.component";

@Component({
    selector: "app-individual-report",
    templateUrl: "./individual-report.component.html",
    styleUrls: ["./individual-report.component.scss"]
})
export class IndividualReportComponent implements OnInit {
    attendanceParams = new PeopleSearchParams();
    monthCalender: any[] = [];
    months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    days = ["Sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday"];
    selectedMonthName = "";
    selectedYear = "";
    currentDate = new Date();
    attendances: PaginatedResult<Attendance>;
    public AttendanceStatusMapping = AttendanceStatusMapping;
    public attendanceStatus = Object.values(AttendanceStatus)
        .filter((value) => typeof value === "number")
        .filter((x) => x != "");
    employeesLookup: Employee[];
    selectedEmployeeId: any;
    constructor(
        public attendanceService: AttendanceService,
        private sttendanceRequestService: AttendanceRequestService,
        public dialog: MatDialog,
        private employeeService: EmployeeService,
        private toastr: ToastrService
    ) {}

    ngOnInit(): void {
        this.bindCalender(this.currentDate);
        this.loadLookups();
    }

    loadLookups() {
        let employeeParams = new SearchParams();
        this.employeeService.getEmployees(employeeParams).subscribe((res) => {
            this.employeesLookup = res.data;
        });
    }

    getData() {
        this.attendanceParams.month = this.currentDate.getMonth() + 1;
        this.attendanceParams.year = this.currentDate.getFullYear();
        this.attendanceParams.employeeId = this.selectedEmployeeId;
        this.attendanceService.getIndividualReport(this.attendanceParams).subscribe((res) => {
            this.attendances = res;
            this.monthCalender = [];
            this.bindCalender(this.currentDate);
        });
    }

    bindCalender(fromDate) {
        var year = fromDate.getFullYear();
        var month = fromDate.getMonth();
        this.selectedMonthName = this.months[month];
        this.selectedYear = year;
        var firstDay = new Date(year, month, 1);
        var lastDay = new Date(year, month + 1, 0);
        while (firstDay.getDay() !== 0) {
            firstDay.setDate(firstDay.getDate() - 1);
        }
        while (lastDay.getDay() !== (0 + 7) % 7) {
            lastDay.setDate(lastDay.getDate() + 1);
        }
        for (var day = firstDay; day < lastDay; day.setDate(day.getDate())) {
            var week = [];
            for (var i = 0; i < 7; i++) {
                var dateObj = {
                    date: day.getDate(),
                    dateString: day.toLocaleDateString(),
                    attendanceDate: new Date(day),
                    status: "",
                    dayClass: "",
                    attendance: null,
                    isOverTime: false,
                    isWrongMonth: false
                };

                if (day.getMonth() != month) {
                    dateObj.dayClass += " wrong-month ";
                    dateObj.isWrongMonth = true;
                }

                if (day.toDateString() === new Date().toDateString()) {
                    dateObj.dayClass += " today ";
                }
                if (this.attendances) {
                    var attendance = this.attendances.data.find((x) => new Date(x.attendanceDate).toDateString() == day.toDateString() && x.attendanceType != AttendanceType.OverTime);
                    if (attendance) {
                        dateObj.status = this.AttendanceStatusMapping[attendance.attendanceStatus];
                        dateObj.attendance = attendance;
                    }
                    var overtime = this.attendances.data.find((x) => new Date(x.attendanceDate).toDateString() == day.toDateString() && x.attendanceType == AttendanceType.OverTime);
                    if (overtime) {
                        dateObj.isOverTime = true;
                    }
                }

                week.push(dateObj);
                day.setDate(day.getDate() + 1);
            }
            this.monthCalender.push(week);
        }
    }

    changeMonth(value) {
        this.currentDate.setMonth(this.currentDate.getMonth() + value, 1);
        this.getData();
    }

    selectionChange($event) {
        this.getData();
    }

    markAttendance(status, dateObj) {
        if (!this.selectedEmployeeId) {
            this.toastr.error("Select an employee..");
            return;
        }
        if (dateObj.isWrongMonth) {
            return;
        }
        if (dateObj.attendance) {
            dateObj.attendance.attendanceStatus = status;

            const dialogRef = this.dialog.open(AttendanceFormComponent, {
                data: dateObj.attendance
            });
            dialogRef.afterClosed().subscribe((result) => {
                this.getData();
            });
        } else {
            if (status == AttendanceStatus.Present) {
                const dialogRef = this.dialog.open(AttendanceLogFormComponent, {
                    data: {
                        employeeId: this.selectedEmployeeId,
                        attendanceDate: dateObj.attendanceDate,
                        attendanceStatus: status
                    }
                });
                dialogRef.afterClosed().subscribe((result) => {
                    this.getData();
                });
            } else {
                var model = <Attendance>{
                    employeeId: this.selectedEmployeeId,
                    attendanceDate: dateObj.attendanceDate,
                    attendanceStatus: status
                };
                this.attendanceService.create(model).subscribe((res) => {
                    this.toastr.success(res.messages[0]);
                    this.getData();
                });
            }
        }
    }

    getCount(status) {
        if (this.attendances) {
            var attendance = this.attendances.data.filter((x) => x.attendanceStatus == status && x.attendanceType != AttendanceType.OverTime);
            return attendance.length;
        }
        return 0;
    }
    getOverTimeCount() {
        if (this.attendances) {
            var attendance = this.attendances.data.filter((x) => x.attendanceType == AttendanceType.OverTime);
            return attendance.length;
        }
        return 0;
    }
}

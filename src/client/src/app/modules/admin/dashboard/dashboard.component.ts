import { HttpParams } from "@angular/common/http";
import { Component, Inject, LOCALE_ID, OnInit, ViewChild } from "@angular/core";
import { MatDatepickerInputEvent } from "@angular/material/datepicker";
import { Dashboard } from "./models/dashboard";
import { DashboardService } from "./services/dashboard.service";

@Component({
    selector: "app-dashboard",
    templateUrl: "./dashboard.component.html",
    styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent implements OnInit {
    attendanceStats: Dashboard;
    departments: any[];
    selectedDate: Date;
    departmentId: any = "";
    constructor(private dashboardService: DashboardService, @Inject(LOCALE_ID) public locale: string) {
        console.log(locale);
    }

    ngOnInit(): void {
        this.selectedDate = new Date();
        this.loadAttendanceStats();
        this.loadLookup();
    }

    loadLookup() {
        this.dashboardService.getDepartments().subscribe((res) => {
            this.departments = res.data;
        });
    }
    loadAttendanceStats() {
        let params = new HttpParams();
        params = params.append("StartDate", this.dashboardService.getDateStringOnly(this.selectedDate));
        params = params.append("departmentId", this.departmentId);
        this.dashboardService.getAttendanceStats(params).subscribe((res) => {
            this.attendanceStats = res;
        });
    }
    dateChange(event: MatDatepickerInputEvent<Date>) {
        this.loadAttendanceStats();
    }
    selectionChange($event){
        this.loadAttendanceStats();
    }
}

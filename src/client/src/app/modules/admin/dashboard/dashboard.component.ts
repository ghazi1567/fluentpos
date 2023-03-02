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
    selectedDate: Date;
    constructor(private dashboardService: DashboardService,
        @Inject(LOCALE_ID) public locale: string) {
            console.log(locale)
        }

    ngOnInit(): void {
        this.selectedDate = new Date();
        this.loadAttendanceStats();
    }

    loadAttendanceStats() {
        let params = new HttpParams();
        params = params.append("StartDate", this.dashboardService.getDateStringOnly(this.selectedDate));
        this.dashboardService.getAttendanceStats(params).subscribe((res) => {
            this.attendanceStats = res;
        });
    }
    dateChange(event: MatDatepickerInputEvent<Date>) {
        this.loadAttendanceStats();
    }
}

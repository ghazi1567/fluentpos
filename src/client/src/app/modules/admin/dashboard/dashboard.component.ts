import { HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { Dashboard } from "./models/dashboard";
import { DashboardService } from "./services/dashboard.service";

@Component({
    selector: "app-dashboard",
    templateUrl: "./dashboard.component.html",
    styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent implements OnInit {
    attendanceStats: Dashboard;
    constructor(private dashboardService: DashboardService) {}

    ngOnInit(): void {
      this.loadAttendanceStats()
    }

    loadAttendanceStats() {
        let params = new HttpParams();
        params = params.append("StartDate", new Date().toJSON());
        this.dashboardService.getAttendanceStats(params).subscribe((res) => {
            this.attendanceStats = res;
        });
    }
}

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
    dashboardStats: Dashboard;
    departments: any[];
    warehouses: any[];
    selectedDate: Date;
    warehouseId: any = "";
    constructor(private dashboardService: DashboardService, @Inject(LOCALE_ID) public locale: string) {
        console.log(locale);
    }

    ngOnInit(): void {
        this.selectedDate = new Date();
        this.loadDashboardStats();
        this.loadLookup();
    }

    loadLookup() {
        this.dashboardService.getWarehouses().subscribe((res) => {
            this.warehouses = res.data;
        });
    }
 
    loadDashboardStats() {
        let params = new HttpParams();
        params = params.append("StartDate", this.dashboardService.getDateStringOnly(this.selectedDate));
        params = params.append("warehouseId", this.warehouseId);
        this.dashboardService.getDashboardStats(params).subscribe((res) => {
            this.dashboardStats = res.data;
        });
    }
    dateChange(event: MatDatepickerInputEvent<Date>) {
        this.loadDashboardStats();
    }
    selectionChange($event) {
        this.loadDashboardStats();
    }
}

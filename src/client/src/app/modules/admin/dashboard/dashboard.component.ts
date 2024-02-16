import { HttpParams } from "@angular/common/http";
import { Component, Inject, LOCALE_ID, OnInit, ViewChild } from "@angular/core";
import { MatDatepickerInputEvent } from "@angular/material/datepicker";
import { Dashboard } from "./models/dashboard";
import { DashboardService } from "./services/dashboard.service";
import { AuthService } from "src/app/core/services/auth.service";
import { LocalStorageService } from "src/app/core/services/local-storage.service";

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
    constructor(private dashboardService: DashboardService, @Inject(LOCALE_ID) public locale: string, private authService: AuthService, private localStorage: LocalStorageService) {
        console.log(locale);
    }

    ngOnInit(): void {
        this.selectedDate = new Date();
        this.loadDashboardStats();
        this.loadLookup();
    }

    loadLookup() {
        this.dashboardService.getUserWarehouse(this.authService.getUserId).subscribe((result) => {
            let warehouses = result.data;
            this.dashboardService.getWarehouses().subscribe((res) => {
                if (warehouses.length == 0) {
                    this.warehouses = res.data;
                } else {
                    this.warehouses = res.data.filter((x) => {
                        var exist = warehouses.find((w) => w.warehouseId == x.id);
                        return exist != null;
                    });
                }
                this.localStorage.setItem("userWarehouse", JSON.stringify(this.warehouses));
                this.localStorage.setItem("warehouseIds", JSON.stringify(this.warehouses.map((item) => item.id)));
            });
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

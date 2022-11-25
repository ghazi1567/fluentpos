import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { DashboardApiService } from "src/app/core/api/dashboard/dashboard-api.service";
import { Dashboard } from "../models/dashboard";

@Injectable({
    providedIn: "root"
})
export class DashboardService {
    constructor(private api: DashboardApiService) {}

    getAttendanceStats(params: HttpParams): Observable<Dashboard> {
        return this.api.getAttendanceStats(params).pipe(map((response: Dashboard) => response));
    }
}

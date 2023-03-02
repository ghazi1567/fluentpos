import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BaseApiService } from "src/app/core/api/baseApiService";
import { DashboardApiService } from "src/app/core/api/dashboard/dashboard-api.service";
import { Dashboard } from "../models/dashboard";

@Injectable({
    providedIn: "root"
})
export class DashboardService extends BaseApiService {
    constructor(private api: DashboardApiService) {
        super();
    }

    getAttendanceStats(params: HttpParams): Observable<Dashboard> {
        return this.api.getAttendanceStats(params).pipe(map((response: Dashboard) => response));
    }
}

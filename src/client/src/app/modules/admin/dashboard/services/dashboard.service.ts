import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BaseApiService } from "src/app/core/api/baseApiService";
import { LookupApiService } from "src/app/core/api/common/lookup.service";
import { DashboardApiService } from "src/app/core/api/dashboard/dashboard-api.service";
import { DepartmentApiService } from "src/app/core/api/org/department-api.service";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Department } from "../../org/models/Department";
import { Dashboard } from "../models/dashboard";
import { Result } from "src/app/core/models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class DashboardService extends BaseApiService {
    constructor(private api: DashboardApiService, private dptApi: LookupApiService) {
        super();
    }

    getAttendanceStats(params: HttpParams): Observable<Dashboard> {
        return this.api.getAttendanceStats(params).pipe(map((response: Dashboard) => response));
    }
    getDashboardStats(params: HttpParams): Observable<Result<Dashboard>> {
        return this.api.getDashboardStats(params).pipe(map((response: Result<Dashboard>) => response));
    }
    getDepartments(): Observable<PaginatedResult<Department>> {
        return this.dptApi.getDepartmentLookup().pipe(map((response: PaginatedResult<Department>) => response));
    }
    getWarehouses(): Observable<PaginatedResult<Department>> {
        return this.dptApi.getWarehouseLookup().pipe(map((response: PaginatedResult<Department>) => response));
    }
}

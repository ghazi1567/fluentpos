import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: "root"
})
export class DashboardApiService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) {}

    getAttendanceStats(params: HttpParams) {
        return this.http.get(this.baseUrl + "people/Dashboard/AttendanceStats", { params: params });
    }
}

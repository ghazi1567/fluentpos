import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: "root"
})
export class ReportServiceApi {
    baseUrl = environment.apiUrl + "invoicing/Reports/";

    constructor(private http: HttpClient) {}

    getVarianceReport(model: any) {
        return this.http.post(this.baseUrl + "VarianceReport", model);
    }
    
}

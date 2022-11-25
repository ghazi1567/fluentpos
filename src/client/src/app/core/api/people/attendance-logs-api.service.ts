import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class AttendanceLogsApiService {
    baseUrl = environment.apiUrl + "people/AttendanceLog";

    constructor(private http: HttpClient) {}


    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get<Result<any>>(this.baseUrl + `/${id}`);
    }

    create(customer: any) {
        return this.http.post(this.baseUrl, customer);
    }

    update(customer: any) {
        return this.http.put(this.baseUrl, customer);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }

    updateApproval(customer: any) {
        return this.http.post(this.baseUrl + "/UpdateApproval", customer);
    }
}

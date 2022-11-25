import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Attendance } from "src/app/modules/admin/people/models/Attendance";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class AttendanceServiceApi {
    baseUrl = environment.apiUrl + "people/Attendance";

    constructor(private http: HttpClient) {}

    getApprovarList(params: HttpParams) {
        return this.http.get(this.baseUrl + "/RequestApprover", { params: params });
    }

    getMyQueue(params: HttpParams) {
        return this.http.get(this.baseUrl + "/MyQueue", { params: params });
    }

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getIndividualReport(params: HttpParams) {
        return this.http.get(this.baseUrl + "/IndividualReport", { params: params });
    }

    getById(id: string) {
        return this.http.get<Result<Attendance>>(this.baseUrl + `/${id}`);
    }

    create(customer: Attendance) {
        return this.http.post(this.baseUrl, customer);
    }

    update(customer: Attendance) {
        return this.http.put(this.baseUrl, customer);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }

    updateApproval(customer: any) {
        return this.http.post(this.baseUrl + "/UpdateApproval", customer);
    }

    advanceSearch(model: any) {
        return this.http.post(this.baseUrl + "/AdvanceSearch", model);
    }
}

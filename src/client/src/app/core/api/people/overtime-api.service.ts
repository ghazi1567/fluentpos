import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EmployeeRequest } from "src/app/modules/admin/people/models/employeeRequest";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class OvertimeApiService {
    baseUrl = environment.apiUrl + "people/OvertimeRequests";

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

    getById(id: string) {
        return this.http.get<Result<EmployeeRequest>>(this.baseUrl + `/${id}`);
    }

    create(customer: EmployeeRequest) {
        return this.http.post(this.baseUrl, customer);
    }

    update(customer: EmployeeRequest) {
        return this.http.put(this.baseUrl, customer);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }

    updateApproval(customer: any) {
        return this.http.post(this.baseUrl + "/UpdateApproval", customer);
    }

    createOvertimePlans(model: any) {
        return this.http.post(this.baseUrl + "/OvertimePlan", model);
    }

    deleteOvertimePlans(id: string) {
        return this.http.delete(this.baseUrl + "/OvertimePlan" + `/${id}`);
    }

    getOvertimePlans(params: HttpParams) {
        return this.http.get(this.baseUrl + "/OvertimePlan", { params: params });
    }
}

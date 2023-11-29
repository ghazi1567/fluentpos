import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, tap } from "rxjs/operators";
import { PaginatedResult } from "../../models/wrappers/PaginatedResult";

@Injectable({
    providedIn: "root"
})
export class LookupApiService {
    baseUrl = environment.apiUrl;
    params = new HttpParams();

    constructor(private http: HttpClient) {
        this.params.append("pageSize", 1000000);
    }

    getJobs() {
        return this.http.get(this.baseUrl + "org/job/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getDepartmentLookup() {
        return this.http.get(this.baseUrl + "org/department/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getDesignationLookup() {
        return this.http.get(this.baseUrl + "org/designation/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getBranchs() {
        return this.http.get(this.baseUrl + "org/branch/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getPolicyLookup() {
        return this.http.get(this.baseUrl + "org/policy/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getSetup() {
        return this.http.get(this.baseUrl + "org/policy/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
    getEmployeesLookup() {
        return this.http.get(this.baseUrl + "people/employees/Lookup", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }

    getWarehouseLookup() {
        return this.http.get(this.baseUrl + "invoicing/lookup/warehouses", { params: this.params }).pipe(map((response: PaginatedResult<any>) => response));
    }
}

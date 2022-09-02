import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class EmployeeApiService {
    baseUrl = environment.apiUrl + "people/employees";

    constructor(private http: HttpClient) {}

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get<Result<Employee>>(this.baseUrl + `/${id}`);
    }

    create(customer: Employee) {
        return this.http.post(this.baseUrl, customer);
    }

    update(customer: Employee) {
        return this.http.put(this.baseUrl, customer);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }
}

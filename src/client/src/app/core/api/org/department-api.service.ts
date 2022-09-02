import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Department } from "src/app/modules/admin/org/models/Department";
import { environment } from "src/environments/environment";
import { IResult } from "../../models/wrappers/IResult";

@Injectable({
    providedIn: "root"
})
export class DepartmentApiService {
    baseUrl = environment.apiUrl + "org/department/";

    constructor(private http: HttpClient) {}

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get<IResult<Department>>(this.baseUrl + id);
    }

    create(user: Department) {
        return this.http.post(this.baseUrl, user);
    }

    update(user: Department) {
        return this.http.put(this.baseUrl, user);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + id);
    }
}

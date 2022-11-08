import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Designation } from "src/app/modules/admin/org/models/designation";
import { environment } from "src/environments/environment";
import { IResult } from "../../models/wrappers/IResult";

@Injectable({
    providedIn: "root"
})
export class JobApiService {
    baseUrl = environment.apiUrl + "org/Job/";

    constructor(private http: HttpClient) {}

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get<IResult<any>>(this.baseUrl + id);
    }

    create(user: any) {
        return this.http.post(this.baseUrl, user);
    }

    runJob(user: any) {
        return this.http.post(this.baseUrl + 'run', user);
    }

    update(user: any) {
        return this.http.put(this.baseUrl, user);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + id);
    }
}

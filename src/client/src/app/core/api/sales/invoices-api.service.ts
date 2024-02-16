import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: "root"
})
export class InvoicesApiService {
    baseUrl = environment.apiUrl + "invoicing/Invoice/";

    constructor(private http: HttpClient) {}

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get(this.baseUrl + id);
    }

    createInvoice(model: any) {
        return this.http.post(`${this.baseUrl}`, model);
    }
}

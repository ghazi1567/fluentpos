import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Order } from "src/app/modules/admin/sales/models/order";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class PurchaseOrderApiService {
    baseUrl = environment.apiUrl + "invoicing/PurchaseOrder/";

    constructor(private http: HttpClient) {}

    getAlls(params: HttpParams) {
        return this.http.get(this.baseUrl, { params: params });
    }

    getById(id: string) {
        return this.http.get<Result<Order>>(this.baseUrl + id);
    }

    getImageById(id: string) {
        return this.http.get(this.baseUrl + `image/${id}`);
    }

    create(product: Order) {
        return this.http.post(this.baseUrl, product);
    }

    update(product: Order) {
        return this.http.put(this.baseUrl, product);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + id);
    }
}

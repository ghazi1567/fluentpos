import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { InvoicesApiService } from "src/app/core/api/sales/invoices-api.service";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Order } from "../models/order";
import { OrderParams } from "../models/orderParams";
import { SearchParams } from "../../org/models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class InvoicesService {
    constructor(private api: InvoicesApiService) {}

    getAlls(orderParams: SearchParams): Observable<PaginatedResult<any>> {
        let params = new HttpParams();
        if (orderParams.searchString) params = params.append("searchString", orderParams.searchString);
        if (orderParams.pageNumber) params = params.append("pageNumber", orderParams.pageNumber.toString());
        if (orderParams.pageSize) params = params.append("pageSize", orderParams.pageSize.toString());
        if (orderParams.orderBy) params = params.append("orderBy", orderParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<any>) => response));
    }

    getById(id: string): Observable<Result<any>> {
        return this.api.getById(id).pipe(map((response: Result<any>) => response));
    }

    createInvoice(model: any): Observable<Result<string>> {
        return this.api.createInvoice(model).pipe(map((response: Result<string>) => response));
    }
}

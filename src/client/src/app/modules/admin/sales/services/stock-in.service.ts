import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { OrderApiService } from "src/app/core/api/sales/order-api.service";
import { StockInApiService } from "src/app/core/api/sales/stock-in-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Order } from "../models/order";
import { OrderParams } from "../models/orderParams";
import { Product } from "../models/product";
import { PurchaseOrderService } from "./purchase-order.service";

@Injectable({
    providedIn: "root"
})
export class StockInService {
    constructor(private purchaseOrderService: PurchaseOrderService, private api: StockInApiService, private orderApi: OrderApiService) {}

    getPurchaseOrders(): Observable<PaginatedResult<Order>> {
        let params = new OrderParams();
        params.pageSize = 10000;
        params.isApproved = "false";
        params.status = 1;
        return this.purchaseOrderService.getOrders(params).pipe(map((response: PaginatedResult<Order>) => response));
    }

    getStockInOrders(orderParams: OrderParams): Observable<PaginatedResult<Order>> {
        let params = new HttpParams();
        if (orderParams.searchString) params = params.append("searchString", orderParams.searchString);
        if (orderParams.pageNumber >= 0) params = params.append("pageNumber", orderParams.pageNumber.toString());
        if (orderParams.pageSize) params = params.append("pageSize", orderParams.pageSize.toString());
        if (orderParams.orderBy) params = params.append("orderBy", orderParams.orderBy.toString());
        if (orderParams.orderType != null) params = params.append("orderType", orderParams.orderType.toString());
        if (orderParams.status != null) params = params.append("status", orderParams.status.toString());
        return this.orderApi.getAlls(params).pipe(map((response: PaginatedResult<Order>) => response));
    }
    getById(id: string): Observable<Result<Order>> {
        return this.api.getById(id).pipe(map((response: Result<Order>) => response));
    }
    create(order: Order): Observable<IResult<Order>> {
        return this.api.create(order).pipe(map((response: IResult<Order>) => response));
    }

    update(order: Order): Observable<IResult<Order>> {
        return this.api.update(order).pipe(map((response: IResult<Order>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    approve(order: any): Observable<IResult<Order>> {
        return this.api.approve(order).pipe(map((response: IResult<Order>) => response));
    }
}

import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ProductApiService } from "src/app/core/api/catalog/product-api.service";
import { PurchaseOrderApiService } from "src/app/core/api/sales/purchase-order-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Product } from "../../catalog/models/product";
import { ProductParams } from "../../catalog/models/productParams";
import { Order } from "../models/order";
import { OrderParams } from "../models/orderParams";

@Injectable({
    providedIn: "root"
})
export class PurchaseOrderService {
    constructor(private api: PurchaseOrderApiService, private productApi: ProductApiService) {}

    getProducts(productParams: ProductParams): Observable<PaginatedResult<Product>> {
        let params = new HttpParams();
        if (productParams.pageSize) {
            params = params.append("pageSize", productParams.pageSize.toString());
        }
        return this.productApi.getAlls(params).pipe(map((response: PaginatedResult<Product>) => response));
    }
   
    getProductById(id: string): Observable<Result<Product>> {
        return this.productApi.getById(id).pipe(map((response: Result<Product>) => response));
    }

    getById(id: string): Observable<Result<Order>> {
        return this.api.getById(id)
          .pipe(map((response: Result<Order>) => response));
      }

    getOrders(orderParams: OrderParams): Observable<PaginatedResult<Order>> {
        let params = new HttpParams();
        if (orderParams.searchString) params = params.append("searchString", orderParams.searchString);
        if (orderParams.pageNumber) params = params.append("pageNumber", orderParams.pageNumber.toString());
        if (orderParams.pageSize) params = params.append("pageSize", orderParams.pageSize.toString());
        if (orderParams.orderBy) params = params.append("orderBy", orderParams.orderBy.toString());
        if (orderParams.isApproved) params = params.append("isApproved", orderParams.isApproved.toString());
        if (orderParams.status != null ) params = params.append("status", orderParams.status.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Order>) => response));
    }

    createPO(order: Order): Observable<IResult<Order>> {
        return this.api.create(order).pipe(map((response: IResult<Order>) => response));
    }

    updatePO(order: Order): Observable<IResult<Order>> {
        return this.api.update(order).pipe(map((response: IResult<Order>) => response));
    }

    deletePO(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

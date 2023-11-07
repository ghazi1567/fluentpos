import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { Order } from '../models/order';
import { OrderParams } from '../models/orderParams';
import { OrderApiService } from 'src/app/core/api/sales/order-api.service';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private api: OrderApiService) {
  }

  getSales(orderParams: OrderParams): Observable<PaginatedResult<Order>> {
    let params = new HttpParams();
    if (orderParams.searchString) params = params.append('searchString', orderParams.searchString);
    if (orderParams.pageNumber) params = params.append('pageNumber', orderParams.pageNumber.toString());
    if (orderParams.pageSize) params = params.append('pageSize', orderParams.pageSize.toString());
    if (orderParams.orderBy) params = params.append('orderBy', orderParams.orderBy.toString());
    return this.api.getAlls(params)
      .pipe(map((response: PaginatedResult<Order>) => response));
  }

  getById(id: string): Observable<Result<Order>> {
    return this.api.getById(id)
      .pipe(map((response: Result<Order>) => response));
  }

  syncOrders(): Observable<Result<Order>> {
    return this.api.syncOrders()
      .pipe(map((response: Result<Order>) => response));
  }
  cancelOrder(model: any): Observable<Result<string>> {
    return this.api.cancelOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  fulFillOrder(model: any): Observable<Result<string>> {
    return this.api.fulFillOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  approveOrder(model: any): Observable<Result<string>> {
    return this.api.approveOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  moveLocation(model: any): Observable<Result<string>> {
    return this.api.moveLocation(model)
      .pipe(map((response: Result<string>) => response));
  }
}

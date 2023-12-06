import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { Order } from '../models/order';
import { OrderParams } from '../models/orderParams';
import { OrderApiService } from 'src/app/core/api/sales/order-api.service';
import { LookupApiService } from 'src/app/core/api/common/lookup.service';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private api: OrderApiService, private lookupApi: LookupApiService) {
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
  getFOById(id: string): Observable<Result<Order>> {
    return this.api.getFOById(id)
      .pipe(map((response: Result<Order>) => response));
  }

  getByOrderNo(id: string): Observable<Result<Order>> {
    return this.api.getByOrderNo(id)
      .pipe(map((response: Result<Order>) => response));
  }

  getOrderForConfirm(id: string): Observable<Result<Order>> {
    return this.api.getOrderForConfirm(id)
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
  confirmOrder(model: any): Observable<Result<string>> {
    return this.api.confirmOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  acceptOrder(model: any): Observable<Result<string>> {
    return this.api.acceptOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  rejectOrder(model: any): Observable<Result<string>> {
    return this.api.rejectOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  requeueOrder(model: any): Observable<Result<string>> {
    return this.api.requeueOrder(model)
      .pipe(map((response: Result<string>) => response));
  }
  scanLoadSheetOrder(model: any): Observable<Result<any>> {
    return this.api.scanLoadSheetOrder(model)
      .pipe(map((response: Result<any>) => response));
  }
  generateLoadSheet(model: any): Observable<Result<any>> {
    return this.api.generateLoadSheet(model)
      .pipe(map((response: Result<any>) => response));
  }
  getLoadsheetInBy(id: string): Observable<Result<any>> {
    return this.api.getLoadsheetInBy(id)
      .pipe(map((response: Result<any>) => response));
  }
  getLoadsheets(): Observable<Result<any>> {
    return this.api.getLoadsheets()
      .pipe(map((response: Result<any>) => response));
  }

  getWarehouseLookup(): Observable<Result<any>> {
    return this.lookupApi.getWarehouseLookup()
      .pipe(map((response: Result<any>) => response));
  }

  getOperationalCityLookup(): Observable<any> {
    return this.lookupApi.getOperationalCityLookup()
      .pipe(map((response: any) => response));
  }

  reGenerateLoadSheet(model: any): Observable<Result<any>> {
    return this.api.reGenerateLoadSheet(model)
      .pipe(map((response: Result<any>) => response));
  }

  getCityCorrectionOrder(): Observable<Result<any>> {
    return this.api.getCityCorrectionOrder()
      .pipe(map((response: Result<any>) => response));
  }
}

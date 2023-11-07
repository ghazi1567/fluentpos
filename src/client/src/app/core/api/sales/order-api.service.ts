import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderApiService {

  baseUrl = environment.apiUrl + 'invoicing/orders/';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, { params: params });
  }

  getById(id: string) {
    return this.http.get(this.baseUrl + id);
  }

  syncOrders() {
    return this.http.get(environment.apiUrl + 'invoicing/Sync/SyncOrders');
  }

  cancelOrder(model: any) {
    return this.http.post(`${this.baseUrl}CancelOrder`, model);
  }

  fulFillOrder(model: any) {
    return this.http.post(`${this.baseUrl}FulFillOrder`, model);
  }
  approveOrder(model: any) {
    return this.http.post(`${this.baseUrl}ApproveOrder`, model);
  }
  moveLocation(model: any) {
    return this.http.post(`${this.baseUrl}MoveLocation`, model);
  }
}

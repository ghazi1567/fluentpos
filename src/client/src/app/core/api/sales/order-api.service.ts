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
  getFOById(id: string) {
    return this.http.get(`${this.baseUrl}fo/${id}`);
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

  getByOrderNo(id: string) {
    var url = `${this.baseUrl}GetByOrderNo/${id}`;
    return this.http.get(url);
  }
  getOrderForConfirm(id: string) {
    var url = `${this.baseUrl}GetOrderForConfirm/${id}`;
    return this.http.get(url);
  }
  confirmOrder(model: any) {
    return this.http.post(`${this.baseUrl}ConfirmOrder`, model);
  }
  acceptOrder(model: any) {
    return this.http.post(`${this.baseUrl}AcceptOrder`, model);
  }
  rejectOrder(model: any) {
    return this.http.post(`${this.baseUrl}RejectOrder`, model);
  }
  requeueOrder(model: any) {
    return this.http.post(`${this.baseUrl}ReQueueOrder`, model);
  }
  scanLoadSheetOrder(model: any) {
    return this.http.post(`${this.baseUrl}ScanLoadSheetOrder`, model);
  }
  generateLoadSheet(model: any) {
    return this.http.post(`${this.baseUrl}GenerateLoadSheet`, model);
  }
  getLoadsheetInBy(id: string) {
    return this.http.get(`${this.baseUrl}GetLoadsheetInBy/${id}`);
  }
  getLoadsheets() {
    return this.http.get(`${this.baseUrl}GetLoadsheets`);
  }
  reGenerateLoadSheet(model: any) {
    return this.http.post(`${this.baseUrl}ReGenerateLoadSheet`, model);
  }
}

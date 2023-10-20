import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Result } from '../../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class SalesApiService {

  baseUrl = environment.apiUrl + 'invoicing/orders/';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, {params: params});
  }

  getById(id: string) {
    return this.http.get(this.baseUrl + id);
  }
  
  syncOrders() {
    return this.http.get(environment.apiUrl + 'invoicing/Sync/SyncOrders');
  }
}

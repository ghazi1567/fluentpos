import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from 'src/app/modules/admin/sales/models/order';
import { environment } from 'src/environments/environment';
import { Result } from '../../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class InventoryLevelApiService {
  baseUrl = environment.apiUrl + "inventory/Level/";

  constructor(private http: HttpClient) { }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, { params: params });
  }

  getById(id: string) {
    return this.http.get<Result<Order>>(this.baseUrl + id);
  }


  create(model: any) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: any) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + id);
  }
}

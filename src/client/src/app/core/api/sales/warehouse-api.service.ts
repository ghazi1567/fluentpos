import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WarehouseApiService {

  baseUrl = environment.apiUrl + 'invoicing/Warehouse/';

  constructor(private http: HttpClient) {
  }

  getAlls() {
    return this.http.get(this.baseUrl);
  }

}

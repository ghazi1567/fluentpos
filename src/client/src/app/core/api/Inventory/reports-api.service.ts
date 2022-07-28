import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportsApiService {
  baseUrl = environment.apiUrl + "inventory/reports/";

    constructor(private http: HttpClient) {}

    getStockReport(model: any) {
        return this.http.post(this.baseUrl + "stockreport", model);
    }
    getStockOutReport(model: any) {
        return this.http.post(this.baseUrl + "stockOutReport", model);
    }
    
}
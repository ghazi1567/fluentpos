import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from 'src/app/modules/pos/models/customer';
import { environment } from 'src/environments/environment';
import { Result } from '../../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class PayslipApiService {

  baseUrl = environment.apiUrl + 'accounting/Payslip';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, {params: params});
  }

  getById(id: string) {
    return this.http.get<Result<any>>(this.baseUrl + `/${id}`);
  }
  runJob(id: string) {
    return this.http.get<Result<any>>(this.baseUrl + `/RunJob/${id}`);
  }

  create(model: any) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: any) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + `/${id}`);
  }
}

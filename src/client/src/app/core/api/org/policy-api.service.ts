import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Policy } from 'src/app/modules/admin/org/models/policy';
import { environment } from 'src/environments/environment';
import { IResult } from '../../models/wrappers/IResult';

@Injectable({
  providedIn: 'root'
})
export class PolicyApiService {
  baseUrl = environment.apiUrl + "org/policy/";

  constructor(private http: HttpClient) {}

  getAlls(params: HttpParams) {
      return this.http.get(this.baseUrl, { params: params });
  }

  getById(id: string) {
      return this.http.get<IResult<Policy>>(this.baseUrl + id);
  }

  create(user: Policy) {
      return this.http.post(this.baseUrl, user);
  }

  update(user: Policy) {
      return this.http.put(this.baseUrl, user);
  }

  delete(id: string) {
      return this.http.delete(this.baseUrl + id);
  }
}

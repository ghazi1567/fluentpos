import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Designation } from 'src/app/modules/admin/org/models/designation';
import { environment } from 'src/environments/environment';
import { IResult } from '../../models/wrappers/IResult';

@Injectable({
  providedIn: 'root'
})
export class DesignationApiService {
  baseUrl = environment.apiUrl + "org/designation/";

  constructor(private http: HttpClient) {}

  getAlls(params: HttpParams) {
      return this.http.get(this.baseUrl, { params: params });
  }

  getById(id: string) {
      return this.http.get<IResult<Designation>>(this.baseUrl + id);
  }

  create(user: Designation) {
      return this.http.post(this.baseUrl, user);
  }

  update(user: Designation) {
      return this.http.put(this.baseUrl, user);
  }

  delete(id: string) {
      return this.http.delete(this.baseUrl + id);
  }
}

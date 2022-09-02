import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Branch } from 'src/app/modules/admin/org/models/branch';
import { environment } from 'src/environments/environment';
import { IResult } from '../../models/wrappers/IResult';

@Injectable({
  providedIn: 'root'
})
export class BranchApiService {

  baseUrl = environment.apiUrl + 'org/branch/';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, {params: params});
  }

  getById(id: string) {
    return this.http.get<IResult<Branch>>(this.baseUrl + id);
  }

  create(user: Branch) {
    return this.http.post(this.baseUrl, user);
  }

  update(user: Branch) {
    return this.http.put(this.baseUrl, user);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + id);
  }

}

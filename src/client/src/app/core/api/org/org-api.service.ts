import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Organization } from 'src/app/modules/admin/org/models/organization';
import { environment } from 'src/environments/environment';
import { IResult } from '../../models/wrappers/IResult';

@Injectable({
  providedIn: 'root'
})
export class OrgApiService {

 
  baseUrl = environment.apiUrl + 'org/setup/';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, {params: params});
  }

  getById(id: string) {
    return this.http.get<IResult<Organization>>(this.baseUrl + id);
  }

  create(user: Organization) {
    return this.http.post(this.baseUrl, user);
  }

  update(user: Organization) {
    return this.http.put(this.baseUrl, user);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + id);
  }


}

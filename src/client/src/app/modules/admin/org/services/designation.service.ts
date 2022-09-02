import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DesignationApiService } from 'src/app/core/api/org/designation-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Designation } from '../models/designation';
import { SearchParams } from '../models/SearchParams';

@Injectable({
  providedIn: 'root'
})
export class DesignationService {
  constructor(private api: DesignationApiService) {}

  getAlls(DesignationParams: SearchParams) {
      let params = new HttpParams();
      if (DesignationParams.searchString) params = params.append("searchString", DesignationParams.searchString);
      if (DesignationParams.pageNumber) params = params.append("pageNumber", DesignationParams.pageNumber.toString());
      if (DesignationParams.pageSize) params = params.append("pageSize", DesignationParams.pageSize.toString());
      if (DesignationParams.orderBy) params = params.append("orderBy", DesignationParams.orderBy.toString());
      return this.api.getAlls(params).pipe(map((response: PaginatedResult<Designation>) => response));
  }

  getById(id: string): Observable<IResult<Designation>> {
      return this.api.getById(id).pipe(map((response: IResult<Designation>) => response));
  }

  create(data: Designation): Observable<IResult<Designation>> {
      return this.api.create(data).pipe(map((response: IResult<Designation>) => response));
  }

  update(data: Designation): Observable<IResult<Designation>> {
      return this.api.update(data).pipe(map((response: IResult<Designation>) => response));
  }

  delete(id: string): Observable<IResult<string>> {
      return this.api.delete(id).pipe(map((response: IResult<string>) => response));
  }
}

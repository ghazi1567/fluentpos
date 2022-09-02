import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BranchApiService } from 'src/app/core/api/org/branch-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Branch } from '../models/branch';
import { SearchParams } from '../models/SearchParams';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  constructor(private api: BranchApiService) {}

  getAlls(branchParams: SearchParams) {
    let params = new HttpParams();
    if (branchParams.searchString)
      params = params.append('searchString', branchParams.searchString);
    if (branchParams.pageNumber)
      params = params.append('pageNumber', branchParams.pageNumber.toString());
    if (branchParams.pageSize)
      params = params.append('pageSize', branchParams.pageSize.toString());
    if (branchParams.orderBy)
      params = params.append('orderBy', branchParams.orderBy.toString());
    return this.api
      .getAlls(params)
      .pipe(map((response: PaginatedResult<Branch>) => response));
  }

  getById(id: string): Observable<IResult<Branch>> {
      return this.api.getById(id).pipe(map((response: IResult<Branch>) => response));
  }

  create(data: Branch): Observable<IResult<Branch>> {
    return this.api
      .create(data)
      .pipe(map((response: IResult<Branch>) => response));
  }

  update(data: Branch): Observable<IResult<Branch>> {
      return this.api.update(data).pipe(map((response: IResult<Branch>) => response));
  }

  delete(id: string): Observable<IResult<string>> {
      return this.api.delete(id).pipe(map((response: IResult<string>) => response));
  }

}
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SalaryPerksApiService } from 'src/app/core/api/payroll/salary-perks-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { SalaryPerks } from '../models/salaryperks';
import { SearchParams } from '../models/SearchParams';

@Injectable({
  providedIn: 'root'
})
export class SalaryPerksService {
  constructor(private api: SalaryPerksApiService) {}

  getAll(CustomerParams: SearchParams): Observable<PaginatedResult<SalaryPerks>> {
      let params = new HttpParams();
      if (CustomerParams.searchString) params = params.append("searchString", CustomerParams.searchString);
      if (CustomerParams.pageNumber) params = params.append("pageNumber", CustomerParams.pageNumber.toString());
      if (CustomerParams.pageSize) params = params.append("pageSize", CustomerParams.pageSize.toString());
      if (CustomerParams.orderBy) params = params.append("orderBy", CustomerParams.orderBy.toString());
      if (CustomerParams.employeeId) params = params.append("employeeId", CustomerParams.employeeId.toString());
      if (CustomerParams.type) params = params.append("type", CustomerParams.type.toString());
      if (CustomerParams.isGlobal) params = params.append("isGlobal", CustomerParams.isGlobal.toString());
      return this.api.getAlls(params).pipe(map((response: PaginatedResult<SalaryPerks>) => response));
  }

  getById(id: string): Observable<Result<SalaryPerks>> {
      return this.api.getById(id).pipe(map((response: Result<SalaryPerks>) => response));
  }

  create(payroll: SalaryPerks): Observable<IResult<SalaryPerks>> {
      return this.api.create(payroll).pipe(map((response: IResult<SalaryPerks>) => response));
  }

  update(payroll: SalaryPerks): Observable<IResult<SalaryPerks>> {
      return this.api.update(payroll).pipe(map((response: IResult<SalaryPerks>) => response));
  }

  delete(id: string): Observable<IResult<string>> {
      return this.api.delete(id).pipe(map((response: IResult<string>) => response));
  }
}

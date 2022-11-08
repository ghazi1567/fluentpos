import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { SalaryApiService } from "src/app/core/api/payroll/salary-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Salary } from "../models/salary";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class SalaryService {
    constructor(private api: SalaryApiService) {}

    getAll(CustomerParams: SearchParams): Observable<PaginatedResult<Salary>> {
        let params = new HttpParams();
        if (CustomerParams.searchString) params = params.append("searchString", CustomerParams.searchString);
        if (CustomerParams.pageNumber) params = params.append("pageNumber", CustomerParams.pageNumber.toString());
        if (CustomerParams.pageSize) params = params.append("pageSize", CustomerParams.pageSize.toString());
        if (CustomerParams.orderBy) params = params.append("orderBy", CustomerParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Salary>) => response));
    }

    getById(id: string): Observable<Result<Salary>> {
        return this.api.getById(id).pipe(map((response: Result<Salary>) => response));
    }

    create(payroll: Salary): Observable<IResult<Salary>> {
        return this.api.create(payroll).pipe(map((response: IResult<Salary>) => response));
    }

    update(payroll: Salary): Observable<IResult<Salary>> {
        return this.api.update(payroll).pipe(map((response: IResult<Salary>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

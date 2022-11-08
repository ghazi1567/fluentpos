import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BaseApiService } from "src/app/core/api/baseApiService";
import { PayrollApiService } from "src/app/core/api/payroll/payroll-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { PayrollRequest } from "../models/payrollRequest";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class PayrollService extends BaseApiService {

    constructor(private api: PayrollApiService) {
        super();
    }

    getAll(CustomerParams: SearchParams): Observable<PaginatedResult<PayrollRequest>> {
        let params = new HttpParams();
        if (CustomerParams.searchString) params = params.append("searchString", CustomerParams.searchString);
        if (CustomerParams.pageNumber) params = params.append("pageNumber", CustomerParams.pageNumber.toString());
        if (CustomerParams.pageSize) params = params.append("pageSize", CustomerParams.pageSize.toString());
        if (CustomerParams.orderBy) params = params.append("orderBy", CustomerParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<PayrollRequest>) => response));
    }

    getById(id: string): Observable<Result<PayrollRequest>> {
        return this.api.getById(id).pipe(map((response: Result<PayrollRequest>) => response));
    }

    runJob(id: string): Observable<Result<PayrollRequest>> {
        return this.api.runJob(id).pipe(map((response: Result<PayrollRequest>) => response));
    }

    create(payroll: PayrollRequest): Observable<IResult<PayrollRequest>> {
        return this.api.create(payroll).pipe(map((response: IResult<PayrollRequest>) => response));
    }

    update(payroll: PayrollRequest): Observable<IResult<PayrollRequest>> {
        return this.api.update(payroll).pipe(map((response: IResult<PayrollRequest>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

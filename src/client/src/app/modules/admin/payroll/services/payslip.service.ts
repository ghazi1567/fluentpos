import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BaseApiService } from "src/app/core/api/baseApiService";
import { PayrollApiService } from "src/app/core/api/payroll/payroll-api.service";
import { PayslipApiService } from "src/app/core/api/payroll/payslip-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Payroll } from "../models/Payroll";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class PayslipService {
    constructor(private api: PayslipApiService) {}

    getAll(CustomerParams: SearchParams): Observable<PaginatedResult<Payroll>> {
        let params = new HttpParams();
        if (CustomerParams.searchString) params = params.append("searchString", CustomerParams.searchString);
        if (CustomerParams.pageNumber) params = params.append("pageNumber", CustomerParams.pageNumber.toString());
        if (CustomerParams.pageSize) params = params.append("pageSize", CustomerParams.pageSize.toString());
        if (CustomerParams.orderBy) params = params.append("orderBy", CustomerParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Payroll>) => response));
    }

    getById(id: string): Observable<Result<Payroll>> {
        return this.api.getById(id).pipe(map((response: Result<Payroll>) => response));
    }

    runJob(id: string): Observable<Result<Payroll>> {
        return this.api.runJob(id).pipe(map((response: Result<Payroll>) => response));
    }

    create(payroll: Payroll): Observable<IResult<Payroll>> {
        return this.api.create(payroll).pipe(map((response: IResult<Payroll>) => response));
    }

    update(payroll: Payroll): Observable<IResult<Payroll>> {
        return this.api.update(payroll).pipe(map((response: IResult<Payroll>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

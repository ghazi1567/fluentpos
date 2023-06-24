import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { OvertimeApiService } from "src/app/core/api/people/overtime-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { EmployeeRequest } from "../models/employeeRequest";
import { PeopleSearchParams } from "../models/peopleSearchParams";

@Injectable({
    providedIn: "root"
})
export class OvertimeService {
    constructor(private api: OvertimeApiService) {}

    getParams(searchParams: PeopleSearchParams): HttpParams {
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        if (searchParams.employeeId) params = params.append("employeeId", searchParams.employeeId.toString());
        if (searchParams.requestId) params = params.append("requestId", searchParams.requestId.toString());
        if (searchParams.requestType) params = params.append("requestType", searchParams.requestType);
        return params;
    }

    getMyQueue(searchParams: PeopleSearchParams): Observable<PaginatedResult<EmployeeRequest>> {
        let params = this.getParams(searchParams);
        return this.api.getMyQueue(params).pipe(map((response: PaginatedResult<EmployeeRequest>) => response));
    }

    getApprovarList(searchParams: PeopleSearchParams): Observable<PaginatedResult<EmployeeRequest>> {
        let params = this.getParams(searchParams);
        return this.api.getApprovarList(params).pipe(map((response: PaginatedResult<EmployeeRequest>) => response));
    }

    getAll(searchParams: PeopleSearchParams): Observable<PaginatedResult<EmployeeRequest>> {
        let params = this.getParams(searchParams);
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<EmployeeRequest>) => response));
    }

    getById(id: string): Observable<Result<EmployeeRequest>> {
        return this.api.getById(id).pipe(map((response: Result<EmployeeRequest>) => response));
    }

    create(model: EmployeeRequest): Observable<IResult<EmployeeRequest>> {
        return this.api.create(model).pipe(map((response: IResult<EmployeeRequest>) => response));
    }

    update(model: EmployeeRequest): Observable<IResult<EmployeeRequest>> {
        return this.api.update(model).pipe(map((response: IResult<EmployeeRequest>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    updateApproval(model: any): Observable<IResult<any>> {
        return this.api.updateApproval(model).pipe(map((response: IResult<any>) => response));
    }

    createOvertimePlan(model: any) {
        return this.api.createOvertimePlans(model).pipe(map((response: IResult<any>) => response));
    }

    deleteOvertimePlan(id: string) {
        return this.api.deleteOvertimePlans(id).pipe(map((response: IResult<string>) => response));
    }

    getOvertimePlans(searchParams: PeopleSearchParams): Observable<PaginatedResult<any>> {
        let params = this.getParams(searchParams);
        return this.api.getOvertimePlans(params).pipe(map((response: PaginatedResult<any>) => response));
    }
}

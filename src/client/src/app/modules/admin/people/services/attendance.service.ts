import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { AttendanceServiceApi } from "src/app/core/api/people/attendance-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Attendance } from "../models/attendance";
import { PeopleSearchParams } from "../models/peopleSearchParams";

@Injectable({
    providedIn: "root"
})
export class AttendanceService {
    constructor(private api: AttendanceServiceApi) {}

    getParams(searchParams: PeopleSearchParams): HttpParams{
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        if (searchParams.employeeId) params = params.append("employeeId", searchParams.employeeId.toString());
        if (searchParams.requestId) params = params.append("requestId", searchParams.requestId.toString());
        if (searchParams.requestType) params = params.append("requestType", searchParams.requestType);
        if (searchParams.month) params = params.append("month", searchParams.month);
        if (searchParams.year) params = params.append("year", searchParams.year);
        return params;
    }
    
    getAll(searchParams: PeopleSearchParams): Observable<PaginatedResult<Attendance>> {
        let params = this.getParams(searchParams);
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Attendance>) => response));
    }

    getIndividualReport(searchParams: PeopleSearchParams): Observable<PaginatedResult<Attendance>> {
        let params = this.getParams(searchParams);
        return this.api.getIndividualReport(params).pipe(map((response: PaginatedResult<Attendance>) => response));
    }

    getById(id: string): Observable<Result<Attendance>> {
        return this.api.getById(id).pipe(map((response: Result<Attendance>) => response));
    }

    create(model: Attendance): Observable<IResult<Attendance>> {
        return this.api.create(model).pipe(map((response: IResult<Attendance>) => response));
    }

    update(model: Attendance): Observable<IResult<Attendance>> {
        return this.api.update(model).pipe(map((response: IResult<Attendance>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    updateApproval(model: any): Observable<IResult<any>> {
        return this.api.updateApproval(model).pipe(map((response: IResult<any>) => response));
    }

    advanceSearch(model: any): Observable<PaginatedResult<Attendance>> {
        return this.api.advanceSearch(model).pipe(map((response: PaginatedResult<Attendance>) => response));
    }
}

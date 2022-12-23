import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { AttendanceLogsApiService } from "src/app/core/api/people/attendance-logs-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { BioAttendance } from "../models/bioAttendance";
import { EmployeeRequest } from "../models/employeeRequest";
import { PeopleSearchParams } from "../models/peopleSearchParams";

@Injectable({
    providedIn: "root"
})
export class AttendanceLogService {
    constructor(private api: AttendanceLogsApiService) {}

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

    getAll(searchParams: PeopleSearchParams): Observable<PaginatedResult<BioAttendance>> {
        let params = this.getParams(searchParams);
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<BioAttendance>) => response));
    }

    getById(id: string): Observable<Result<BioAttendance>> {
        return this.api.getById(id).pipe(map((response: Result<BioAttendance>) => response));
    }

    create(model: BioAttendance): Observable<IResult<BioAttendance>> {
        return this.api.create(model).pipe(map((response: IResult<BioAttendance>) => response));
    }

    update(model: BioAttendance): Observable<IResult<BioAttendance>> {
        return this.api.update(model).pipe(map((response: IResult<BioAttendance>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    updateApproval(model: any): Observable<IResult<any>> {
        return this.api.updateApproval(model).pipe(map((response: IResult<any>) => response));
    }

    advanceSearch(model: PeopleSearchParams): Observable<PaginatedResult<BioAttendance>> {
        return this.api.advanceSearch(model).pipe(map((response: PaginatedResult<BioAttendance>) => response));
    }
}

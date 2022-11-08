import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { DesignationApiService } from "src/app/core/api/org/designation-api.service";
import { JobApiService } from "src/app/core/api/org/job-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Designation } from "../models/designation";
import { Job } from "../models/Job";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class JobService {
    constructor(private api: JobApiService) {}

    getAlls(DesignationParams: SearchParams) {
        let params = new HttpParams();
        if (DesignationParams.searchString) params = params.append("searchString", DesignationParams.searchString);
        if (DesignationParams.pageNumber) params = params.append("pageNumber", DesignationParams.pageNumber.toString());
        if (DesignationParams.pageSize) params = params.append("pageSize", DesignationParams.pageSize.toString());
        if (DesignationParams.orderBy) params = params.append("orderBy", DesignationParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Job>) => response));
    }

    getById(id: string): Observable<IResult<Job>> {
        return this.api.getById(id).pipe(map((response: IResult<Job>) => response));
    }

    create(data: Job): Observable<IResult<Job>> {
        return this.api.create(data).pipe(map((response: IResult<Job>) => response));
    }

    update(data: Job): Observable<IResult<Job>> {
        return this.api.update(data).pipe(map((response: IResult<Job>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    runJob(data: any): Observable<IResult<Job>> {
        return this.api.runJob(data).pipe(map((response: IResult<Job>) => response));
    }
}

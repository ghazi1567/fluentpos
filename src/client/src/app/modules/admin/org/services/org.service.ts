import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { OrgApiService } from "src/app/core/api/org/org-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Organization } from "../models/organization";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class OrgService {
    constructor(private api: OrgApiService) {}

    getAlls(searchParams: SearchParams) {
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Organization>) => response));
    }

    getById(id: string): Observable<IResult<Organization>> {
        return this.api.getById(id).pipe(map((response: IResult<Organization>) => response));
    }

    create(data: Organization): Observable<IResult<Organization>> {
        return this.api.create(data).pipe(map((response: IResult<Organization>) => response));
    }

    update(data: Organization): Observable<IResult<Organization>> {
        return this.api.update(data).pipe(map((response: IResult<Organization>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

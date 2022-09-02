import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PolicyApiService } from "src/app/core/api/org/policy-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Policy } from "../models/policy";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class PolicyService {
    constructor(private api: PolicyApiService) {}

    getAlls(PolicyParams: SearchParams) {
        let params = new HttpParams();
        if (PolicyParams.searchString) params = params.append("searchString", PolicyParams.searchString);
        if (PolicyParams.pageNumber) params = params.append("pageNumber", PolicyParams.pageNumber.toString());
        if (PolicyParams.pageSize) params = params.append("pageSize", PolicyParams.pageSize.toString());
        if (PolicyParams.orderBy) params = params.append("orderBy", PolicyParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Policy>) => response));
    }

    getById(id: string): Observable<IResult<Policy>> {
        return this.api.getById(id).pipe(map((response: IResult<Policy>) => response));
    }

    create(data: Policy): Observable<IResult<Policy>> {
        return this.api.create(data).pipe(map((response: IResult<Policy>) => response));
    }

    update(data: Policy): Observable<IResult<Policy>> {
        return this.api.update(data).pipe(map((response: IResult<Policy>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

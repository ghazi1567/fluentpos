import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { DepartmentApiService } from "src/app/core/api/org/department-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Department } from "../models/Department";
import { SearchParams } from "../models/SearchParams";

@Injectable({
    providedIn: "root"
})
export class DepartmentService {
    constructor(private api: DepartmentApiService) {}

    getAlls(DepartmentParams: SearchParams) {
        let params = new HttpParams();
        if (DepartmentParams.searchString) params = params.append("searchString", DepartmentParams.searchString);
        if (DepartmentParams.pageNumber) params = params.append("pageNumber", DepartmentParams.pageNumber.toString());
        if (DepartmentParams.pageSize) params = params.append("pageSize", DepartmentParams.pageSize.toString());
        if (DepartmentParams.orderBy) params = params.append("orderBy", DepartmentParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Department>) => response));
    }

    getById(id: string): Observable<IResult<Department>> {
        return this.api.getById(id).pipe(map((response: IResult<Department>) => response));
    }

    create(data: Department): Observable<IResult<Department>> {
        return this.api.create(data).pipe(map((response: IResult<Department>) => response));
    }

    update(data: Department): Observable<IResult<Department>> {
        return this.api.update(data).pipe(map((response: IResult<Department>) => response));
    }

    delete(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }
}

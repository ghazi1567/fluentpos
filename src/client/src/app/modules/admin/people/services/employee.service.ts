import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { DepartmentApiService } from "src/app/core/api/org/department-api.service";
import { DesignationApiService } from "src/app/core/api/org/designation-api.service";
import { PolicyApiService } from "src/app/core/api/org/policy-api.service";
import { EmployeeApiService } from "src/app/core/api/people/employee-api.service";
import { IResult } from "src/app/core/models/wrappers/IResult";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { Policy } from "../../org/models/policy";
import { SearchParams } from "../../org/models/SearchParams";
import { Employee } from "../models/employee";

@Injectable({
    providedIn: "root"
})
export class EmployeeService {
    constructor(private api: EmployeeApiService, private policyApi: PolicyApiService, private departmentApi: DepartmentApiService, private designationApi: DesignationApiService) {}

    getDepartmentLookup(searchParams: SearchParams): Observable<PaginatedResult<Policy>> {
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        return this.departmentApi.getAlls(params).pipe(map((response: PaginatedResult<Policy>) => response));
    }
    getDesignationLookup(searchParams: SearchParams): Observable<PaginatedResult<Policy>> {
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        return this.designationApi.getAlls(params).pipe(map((response: PaginatedResult<Policy>) => response));
    }
    getPolicyLookup(searchParams: SearchParams): Observable<PaginatedResult<Policy>> {
        let params = new HttpParams();
        if (searchParams.searchString) params = params.append("searchString", searchParams.searchString);
        if (searchParams.pageNumber) params = params.append("pageNumber", searchParams.pageNumber.toString());
        if (searchParams.pageSize) params = params.append("pageSize", searchParams.pageSize.toString());
        if (searchParams.orderBy) params = params.append("orderBy", searchParams.orderBy.toString());
        return this.policyApi.getAlls(params).pipe(map((response: PaginatedResult<Policy>) => response));
    }
    getEmployeesLookup(EmployeeParams: SearchParams): Observable<PaginatedResult<Employee>> {
        let params = new HttpParams();
        if (EmployeeParams.searchString) params = params.append("searchString", EmployeeParams.searchString);
        if (EmployeeParams.pageNumber) params = params.append("pageNumber", EmployeeParams.pageNumber.toString());
        if (EmployeeParams.pageSize) params = params.append("pageSize", EmployeeParams.pageSize.toString());
        if (EmployeeParams.orderBy) params = params.append("orderBy", EmployeeParams.orderBy.toString());
        return this.api.getLookup(params).pipe(map((response: PaginatedResult<Employee>) => response));
    }
    getEmployees(EmployeeParams: SearchParams): Observable<PaginatedResult<Employee>> {
        let params = new HttpParams();
        if (EmployeeParams.searchString) params = params.append("searchString", EmployeeParams.searchString);
        if (EmployeeParams.pageNumber) params = params.append("pageNumber", EmployeeParams.pageNumber.toString());
        if (EmployeeParams.pageSize) params = params.append("pageSize", EmployeeParams.pageSize.toString());
        if (EmployeeParams.orderBy) params = params.append("orderBy", EmployeeParams.orderBy.toString());
        return this.api.getAlls(params).pipe(map((response: PaginatedResult<Employee>) => response));
    }

    getEmployeeById(id: string): Observable<Result<Employee>> {
        return this.api.getById(id).pipe(map((response: Result<Employee>) => response));
    }

    createEmployee(Employee: Employee): Observable<IResult<Employee>> {
        return this.api.create(Employee).pipe(map((response: IResult<Employee>) => response));
    }

    updateEmployee(Employee: Employee): Observable<IResult<Employee>> {
        return this.api.update(Employee).pipe(map((response: IResult<Employee>) => response));
    }

    deleteEmployee(id: string): Observable<IResult<string>> {
        return this.api.delete(id).pipe(map((response: IResult<string>) => response));
    }

    importEmployee(Employee: any): Observable<IResult<Employee>> {
        return this.api.import(Employee).pipe(map((response: IResult<Employee>) => response));
    }

    advanceSearch(model: any): Observable<PaginatedResult<Employee>> {
        return this.api.advanceSearch(model).pipe(map((response: PaginatedResult<Employee>) => response));
    }
}

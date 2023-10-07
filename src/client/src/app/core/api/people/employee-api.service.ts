import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { environment } from "src/environments/environment";
import { Result } from "../../models/wrappers/Result";

@Injectable({
    providedIn: "root"
})
export class EmployeeApiService {
    baseUrl = environment.apiUrl + "people/employees";

    constructor(private http: HttpClient) {}

    getLookup(params: HttpParams) {
        return this.http.get(this.baseUrl+'/Lookup', { params: params }).pipe(
            tap((response) => {
            })
        );
    }
    getAlls(params: HttpParams) {
        // if(localStorage.getItem(this.baseUrl)){
        //     console.log('request already in progress')
        //     return new Observable();
        // }
        localStorage.setItem(this.baseUrl,JSON.stringify(params));
        return this.http.get(this.baseUrl, { params: params }).pipe(
            tap((response) => {
                localStorage.removeItem(this.baseUrl)
            })
        );
    }

    getById(id: string) {
        return this.http.get<Result<Employee>>(this.baseUrl + `/${id}`);
    }

    create(customer: Employee) {
        return this.http.post(this.baseUrl, customer);
    }

    update(customer: Employee) {
        return this.http.put(this.baseUrl, customer);
    }

    delete(id: string) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }

    import(customer: any) {
        return this.http.post(this.baseUrl + `/Import`, customer).pipe(
            tap((response) => {
                console.log(response);
            })
        );
    }

    advanceSearch(model: any) {
        return this.http.post(this.baseUrl + `/AdvanceSearch`, model);
    }
    
    assignDepartment(model: any) {
        return this.http.post(this.baseUrl + `/AssignDepartment`, model);
    }
}

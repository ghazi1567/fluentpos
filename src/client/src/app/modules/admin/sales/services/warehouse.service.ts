import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { WarehouseApiService } from "src/app/core/api/sales/warehouse-api.service";
import { Result } from "src/app/core/models/wrappers/Result";
import { Order } from "../models/order";
import { Warehouse } from "../models/warehouse";

@Injectable({
    providedIn: "root"
})
export class WarehouseService {
    constructor(private api: WarehouseApiService) {}

    getAll(): Observable<Result<Warehouse[]>> {
        return this.api.getAlls().pipe(map((response: Result<Warehouse[]>) => response));
    }
}

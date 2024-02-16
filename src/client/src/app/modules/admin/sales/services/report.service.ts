import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ProductApiService } from "src/app/core/api/catalog/product-api.service";
import { ReportsApiService } from "src/app/core/api/Inventory/reports-api.service";
import { ReportServiceApi } from "src/app/core/api/sales/report.service";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { StockReport } from "../../catalog/models/stockReport";
import { VaraintReport } from "../../catalog/models/variantReport";
import { Order } from "../models/order";
import { OrderParams } from "../models/orderParams";

@Injectable({
    providedIn: "root"
})
export class ReportService {
    constructor(private api: ReportServiceApi,
        private productApiService: ProductApiService,
        private reportsApiService: ReportsApiService) { }

    getVarianceReport(model: any): Observable<Result<VaraintReport[]>> {
        return this.api.getVarianceReport(model).pipe(map((response: any) => response));
    }
    updatePromotion(model: any): Observable<Result<any>> {
        return this.productApiService.updatePromotion(model).pipe(map((response: any) => response));
    }
    getStockReport(model: any): Observable<PaginatedResult<StockReport[]>> {
        return this.reportsApiService.getStockReport(model).pipe(map((response: any) => response));
    }
    getStockOutReport(model: any): Observable<Result<StockReport[]>> {
        return this.reportsApiService.getStockOutReport(model).pipe(map((response: any) => response));
    }

}

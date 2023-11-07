import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { InventoryLevelApiService } from 'src/app/core/api/Inventory/inventory-level-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { OrderParams } from '../models/orderParams';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { SearchParams } from 'src/app/core/models/Filters/SearchParams';

@Injectable({
  providedIn: 'root'
})
export class InventoryLevelService {

  constructor(private api: InventoryLevelApiService) { }

  ImportFile(model: any): Observable<IResult<any>> {
    return this.api.create(model).pipe(map((response: IResult<any>) => response));
  }

  getAlls(orderParams: SearchParams): Observable<PaginatedResult<any>> {
    let params = new HttpParams();
    if (orderParams.searchString) params = params.append('searchString', orderParams.searchString);
    if (orderParams.pageNumber) params = params.append('pageNumber', orderParams.pageNumber.toString());
    if (orderParams.pageSize) params = params.append('pageSize', orderParams.pageSize.toString());
    if (orderParams.orderBy) params = params.append('orderBy', orderParams.orderBy.toString());
    return this.api.getAlls(params).pipe(map((response: PaginatedResult<any>) => response));
  }
}

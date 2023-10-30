import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { InventoryLevelApiService } from 'src/app/core/api/Inventory/inventory-level-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';

@Injectable({
  providedIn: 'root'
})
export class InventoryLevelService {

  constructor(private api: InventoryLevelApiService) { }

  ImportFile(model: any): Observable<IResult<any>> {
    return this.api.create(model).pipe(map((response: IResult<any>) => response));
  }
}

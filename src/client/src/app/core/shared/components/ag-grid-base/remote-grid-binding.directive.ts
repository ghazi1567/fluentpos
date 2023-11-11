import { Directive, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { GridApi, IDatasource, IGetRowsParams } from 'ag-grid-community';
import { EMPTY, Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { RemoteGridApi } from './ag-grid.models';


@Directive({
  selector: '[appRemoteGridBinding]'
})
export class RemoteGridBindingDirective {
  @Input()
  appRemoteGridBinding: RemoteGridApi;

  @Output()
  remoteGridReady = new EventEmitter();

  constructor() { }

  @HostListener('gridReady', ['$event']) gridReady(gridApi: GridApi) {
    this.updateGridApi(gridApi);
  }

  updateGridApi(gridApi) {
    gridApi.api.setDatasource(this.dataSource);
    this.remoteGridReady.emit(gridApi);
  }

  handleError(err) {
    this.appRemoteGridBinding.getDataError(err);
    return EMPTY;
  }

  dataSource: IDatasource = {
    getRows: (params: IGetRowsParams) => {

      this.appRemoteGridBinding
        .getData(params)
        .pipe(
          tap(({ data, totalCount }) => {
            params.successCallback(data, totalCount)
          }
          ),
          catchError(err => this.handleError(err))
        )
        .subscribe();
    }
  };
}

import { Directive, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { GridApi, IDatasource, IGetRowsParams, IServerSideDatasource, IServerSideGetRowsParams, IServerSideGetRowsRequest } from 'ag-grid-community';
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
    gridApi.api.setServerSideDatasource(this.dataSource);
    this.remoteGridReady.emit(gridApi);
  }

  handleError(err) {
    this.appRemoteGridBinding.getDataError(err);
    return EMPTY;
  }

  // dataSource1: IDatasource = {
  //   getRows: (params: IGetRowsParams) => {

  //     this.appRemoteGridBinding
  //       .getData(params)
  //       .pipe(
  //         tap(({ data, totalCount }) => {
  //           params.successCallback(data, totalCount)
  //         }
  //         ),
  //         catchError(err => this.handleError(err))
  //       )
  //       .subscribe();
  //   }
  // };

  dataSource: IServerSideDatasource = {
    getRows: (params: IServerSideGetRowsParams) => {

      this.appRemoteGridBinding
        .getData(params)
        .pipe(
          tap(({ data, totalCount }) => {
            var lastRow = this.getLastRowIndex(params.request, data);
            params.success({
              rowData: data,
              rowCount: 6,
            })
          }
          ),
          catchError(err => this.handleError(err))
        )
        .subscribe();
    }
  };
  getLastRowIndex(request: IServerSideGetRowsRequest, results: any[]) {
    if (!results) return undefined;
    var currentLastRow = (request.startRow || 0) + results.length;
    // if on or after the last block, work out the last row, otherwise return 'undefined'
    return currentLastRow < (request.endRow || 0) ? currentLastRow : undefined;
  }
}

import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Product } from "../../models/product";
import { ProductParams } from "../../models/productParams";
import { ProductService } from "../../services/product.service";
import { ProductFormComponent } from "./product-form/product-form.component";
import { ToastrService } from "ngx-toastr";
import { Sort } from "@angular/material/sort";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { ProductViewComponent } from "./product-view/product-view.component";
import { ReportService } from "../../../sales/services/report.service";
import { WarehouseService } from "../../../sales/services/warehouse.service";
import { CsvParserService } from "src/app/core/services/csv-parser.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { RemoteGridApi } from "src/app/core/shared/components/ag-grid-base/ag-grid.models";
import { EMPTY, Observable } from "rxjs";
import { GridApi, GridOptions, IGetRowsParams, IServerSideDatasource, IServerSideGetRowsParams } from "ag-grid-community";
import { tap, catchError } from "rxjs/operators";
import { AgGridAngular } from "ag-grid-angular";
import {
    ColDef,
    IDateFilterParams,
    INumberFilterParams,
    ITextFilterParams,
} from '@ag-grid-community/core';
@Component({
    selector: "app-product",
    templateUrl: "./product.component.html",
    styleUrls: ["./product.component.scss"]
})
export class ProductComponent implements OnInit {
    products: PaginatedResult<Product>;
    productColumns: ColDef[];
    productParams = new ProductParams();
    searchString: string;
    warehouseLookups: any[];
    productData: Product[];
    availableStock: any[];
    stockReport: Map<any, any>;
    pageSize = 10;
    enableServerSideFilter: boolean = true;
    enableServerSideSorting: boolean = true;
    public paginationPageSize = 20;
    public cacheBlockSize = 10;

    @ViewChild('myGrid') myGrid: AgGridAngular;

    gridOptions: Partial<GridOptions>;
    gridColumnApi;
    columnDefs;
    cacheOverflowSize;
    maxConcurrentDatasourceRequests;
    infiniteInitialRowCount;

    constructor(
        public productService: ProductService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public reportService: ReportService,
        private warehouseService: WarehouseService,
        private csvParserService: CsvParserService
    ) { }

    ngOnInit(): void {
        this.cacheOverflowSize = 2;
        this.maxConcurrentDatasourceRequests = 2;
        this.infiniteInitialRowCount = 2;

        this.gridOptions = {
            headerHeight: 45,
            rowHeight: 30,
            cacheBlockSize: 10,
            paginationPageSize: 10,
            rowModelType: 'infinite',
        }

        // this.loadLookups();

        // this.initColumns();
        this.initOvertimeColumns();
    }
    loadLookups() {
        this.warehouseService.getAll().subscribe((res) => {
            this.warehouseLookups = res.data;
        });
    }

    getProducts(): void {
        this.productParams.pageNumber = 0;
        this.productParams.pageSize = 1000000;
        this.productService.getProducts(this.productParams).subscribe((result) => {
            this.products = result;
            this.productsData = result.data;
        });
    }
    syncProducts(): void {
        this.productService.syncProducts().subscribe((result) => {
            this.toastr.success('Product sync job started in background.')
        });
    }


    openForm(product?: Product): void {
        const dialogRef = this.dialog.open(ProductFormComponent, {
            data: product
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getProducts();
            }
        });
    }


    view(product?: any): void {
        const dialogRef = this.dialog.open(ProductViewComponent, {
            data: product
        });
        dialogRef.afterClosed().subscribe((result) => { });
    }


    productsData: any[] = [];

    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridApi: GridApi;
    rowModelType = "infinite";
    remoteGridBinding = this;
    onGridReady(event): void {
        var params = event.params;
        var baseGrid = event.baseGrid;

        // this.gridApi = event.params.api;
        // this.getProducts();
        this.gridApi = params.api;
        this.gridColumnApi = params.columnApi;

        var datasource = {
            getRows: (params: IGetRowsParams) => {
                var filterSortModel = baseGrid.setFilterSortModel(params);
                console.log(filterSortModel);
                //  TODO: Call a service that fetches list of users
                console.log("Fetching startRow " + params.startRow + " of " + params.endRow);
                var pageNumber = (params.startRow / this.pageSize) + 1;
                this.productParams.pageNumber = pageNumber;
                this.productParams.pageSize = this.pageSize;
                this.productParams.bypassCache = true;
                this.productParams.advanceFilters = filterSortModel.listOfFilters;
                this.productParams.sortModel = filterSortModel.listOfSort;
                console.log(params);
                this.productService
                    .getProducts(this.productParams)
                    .subscribe(res => {
                        params.successCallback(res.data, res.totalCount)
                    });
            }
        }
        this.gridApi.setDatasource(datasource);
    }
    initOvertimeColumns(): void {
        this.productColumns = [
            {
                headerName: "Shopify Id", field: "shopifyId", sortable: true, width: 256,
                filter: 'agNumberColumnFilter',
                filterParams: {
                    maxNumConditions: 1,
                    numAlwaysVisibleConditions: 1,
                    defaultJoinOperator: 'OR',
                } as INumberFilterParams,
            },
            {
                headerName: "Title", field: "title", sortable: true, width: 256, suppressMenu: true,
                filter: 'agTextColumnFilter',
                filterParams: {
                    filterOptions: ['contains'],
                    defaultOption: 'contains',
                    maxNumConditions: 1,
                    numAlwaysVisibleConditions: 1,
                    defaultJoinOperator: 'OR',
                } as ITextFilterParams,
            },
            { headerName: "Vendor", field: "vendor", sortable: true, width: 160, suppressMenu: true, floatingFilter: false },
            { headerName: "Product Type", field: "productType", sortable: true, width: 160 },
            { headerName: "PublishedScope", field: "publishedScope", sortable: true, width: 330 },
            { headerName: "Status", field: "status", sortable: true, width: 330 },
            { headerName: "Tags", field: "tags", sortable: true, width: 330 },
        ];
    }

    onPaginationChanged($event) {
        console.log($event)
    }
    getData(params): Observable<{ data; totalCount }> {
        console.log(params);
        this.productParams.pageNumber = params.request.startRow;
        this.productParams.pageSize = this.pageSize;
        this.productParams.bypassCache = true;
        return this.productService.getProducts(this.productParams)
    }

    serverSideDatasource = () => {
        getRows: (params: IServerSideGetRowsParams) => {
            console.log(params)
            this.productParams.pageNumber = params.request.startRow;
            this.productParams.pageSize = this.paginationPageSize;
            this.productParams.bypassCache = true;

            this.productService
                .getProducts(this.productParams)
                .subscribe(res => {
                    params.successCallback(res.data, res.totalCount)
                });
        }
    };
}

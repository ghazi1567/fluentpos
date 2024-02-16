import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { Result } from "src/app/core/models/wrappers/Result";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { ProductParams } from "../../../catalog/models/productParams";
import { StockReport } from "../../../catalog/models/stockReport";
import { PurchaseOrderService } from "../../services/purchase-order.service";
import { ReportService } from "../../services/report.service";
import { WarehouseService } from "../../services/warehouse.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { ColDef, GridApi, GridOptions, IGetRowsParams } from "ag-grid-community";
import { OrderParams } from "../../models/orderParams";

@Component({
    selector: "app-stock-report",
    templateUrl: "./stock-report.component.html",
    styleUrls: ["./stock-report.component.scss"]
})
export class StockReportComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;
    productLookups: any[];
    filteredproducts: any[];
    displayedColumns: string[] = ["productName", "warehouseName", "AvailableQuantity"];
    dataSource: Result<StockReport[]>;
    selectedProduct: any = {
        productName: "",
        warehouseId: ""
    };
    warehouseLookups: any[];

    constructor(
        public reportService: ReportService,
        private purchaseOrderApi: PurchaseOrderService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        private warehouseService: WarehouseService
    ) { }

    gridApi: GridApi;
    enableServerSideFilter: boolean = true;
    enableServerSideSorting: boolean = true;
    rowModelType = "infinite";
    pageSize = 10;

    ngOnInit(): void {

        this.initStockReportColumns();
    }
    loadLookups() {
        let model = <ProductParams>{
            pageSize: 10000
        };
        this.purchaseOrderApi.getProducts(model).subscribe((res) => {
            this.productLookups = res.data;
        });
        this.warehouseService.getAll().subscribe((res) => {
            this.warehouseLookups = res.data;
        });
    }
    displayProduct(product) {
        if (product) {
            return product.name;
        }
        return "";
    }
    filterProduct(value: any) {
        if (this.selectedProduct.productName) {
            const filterValue = this.selectedProduct.productName.toLowerCase();
            this.filteredproducts = this.productLookups.filter((option) => option.name.toLowerCase().includes(filterValue) || option.barcodeSymbology.toLowerCase().includes(filterValue));
        }
    }


    stockReportColumns: any[] = [];
    stockReportData: any[] = [];
    enablePivotMode = false
    public autoGroupColumnDef: ColDef = {
        minWidth: 250,
    };
    initStockReportColumns(): void {
        this.stockReportColumns = [
            { headerName: "Product", field: "title", sortable: true, isShowable: true, width: 235, filter: false },
            { headerName: "SKU", field: "sku", sortable: true, isShowable: true, },
            { headerName: "Warehouse", field: "warehouseName", sortable: true, isShowable: true, filter: false },
            { headerName: "Committed", field: "committed", sortable: true, filter: 'agNumberColumnFilter', aggFunc: 'sum', type: 'numericColumn', width: 120, },
            { headerName: "Available", field: "availableQuantity", sortable: true, filter: 'agNumberColumnFilter', aggFunc: 'sum', type: 'numericColumn', width: 120, },
            { headerName: "On Hand", field: "onHand", sortable: true, filter: 'agNumberColumnFilter', aggFunc: 'sum', type: 'numericColumn', width: 120, },
            { headerName: "Rack", field: "rack", sortable: true, isShowable: true, width: 120, },
            { headerName: "Last Update", field: "lastUpdatedOn", sortable: true, isShowable: true, },

        ];
    }

    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }


    gridReady(event): void {
        var params = event.params;
        var baseGrid = event.baseGrid;
        this.gridApi = params.api;

        var datasource = {
            getRows: (params: IGetRowsParams) => {
                var filterSortModel = baseGrid.setFilterSortModel(params);
                console.log(filterSortModel);
                var pageNumber = (params.startRow / this.pageSize) + 1;
                this.orderParams.pageNumber = pageNumber;
                this.orderParams.pageSize = this.pageSize;
                this.orderParams.bypassCache = true;
                this.orderParams.advanceFilters = filterSortModel.listOfFilters;
                this.orderParams.sortModel = filterSortModel.listOfSort;

                this.reportService
                    .getStockReport(this.orderParams)
                    .subscribe(res => {
                        params.successCallback(res.data, res.totalCount)
                    });
            }
        }
        this.gridApi.setDatasource(datasource);
        // this.getStockReport();
    }

    getStockReport() {
        this.reportService.getStockReport(this.orderParams).subscribe((result) => {
            this.stockReportData = result.data;
        });
    }

    onFilterTextBoxChanged() {
        this.AgGrid.gridApi.setQuickFilter(
            (document.getElementById('filter-text-box') as HTMLInputElement).value
        );
    }
    onRefresh() {
        this.getStockReport();
    }
}

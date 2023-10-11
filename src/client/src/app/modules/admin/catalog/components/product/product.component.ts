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

@Component({
    selector: "app-product",
    templateUrl: "./product.component.html",
    styleUrls: ["./product.component.scss"]
})
export class ProductComponent implements OnInit {
    products: PaginatedResult<Product>;
    productColumns: any[];
    productParams = new ProductParams();
    searchString: string;
    warehouseLookups: any[];
    productData: Product[];
    availableStock: any[];
    stockReport: Map<any, any>;

    constructor(
        public productService: ProductService,
        public dialog: MatDialog,
        public toastr: ToastrService,
        public reportService: ReportService,
        private warehouseService: WarehouseService,
        private csvParserService: CsvParserService
    ) { }

    ngOnInit(): void {
        // this.loadLookups();

        // this.initColumns();
        this.initOvertimeColumns();
    }
    loadLookups() {
        this.warehouseService.getAll().subscribe((res) => {
            this.warehouseLookups = res.data;
            this.getStockReport();
        });
    }
    getProducts(): void {
        this.productService.getProducts(this.productParams).subscribe((result) => {
            result.data.forEach((p) => {
                p.availableStock = this.stockReport.get(p.id);
            });
            this.products = result;
            this.productData = result.data;
        });
    }

    initColumns1(): void {
        this.productColumns = [
            //{ name: 'Id', dataKey: 'id', isSortable: true, isShowable: true },
            { name: "Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "Product Code", dataKey: "productCode", isSortable: true, isShowable: true },
            { name: "Barcode", dataKey: "barcodeSymbology", isSortable: true, isShowable: true },
            { name: "Location", dataKey: "location", isSortable: true, isShowable: true },
            { name: "Location2", dataKey: "location2", isSortable: true, isShowable: true },
            { name: "Detail", dataKey: "detail", isSortable: true, isShowable: true },
            { name: "Price", dataKey: "price", isSortable: true, isShowable: true },
            { name: "Available Stock", dataKey: "availableStock", isSortable: true, isShowable: true },
            { name: "Discount Factor", dataKey: "discountFactor", isSortable: true, isShowable: true },
            // { name: 'Tax', dataKey: 'tax', isSortable: true , isShowable: true},
            // { name: 'TaxMethod', dataKey: 'taxMethod', isSortable: false , isShowable: false},
            //{ name: 'BarcodeSymbology', dataKey: 'barcodeSymbology', isSortable: true, isShowable: true },
            //{ name: 'IsAlert', dataKey: 'isAlert', isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.productParams.pageNumber = event.pageNumber;
        this.productParams.pageSize = event.pageSize;
        this.getProducts();
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

    remove($event: string): void {
        this.productService.deleteProduct($event).subscribe(() => {
            this.getProducts();
            this.toastr.info("Product Removed");
        });
    }

    sort($event: Sort): void {
        this.productParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.productParams.orderBy);
        this.getProducts();
    }

    filter($event: string): void {
        this.productParams.searchString = $event.trim().toLocaleLowerCase();
        this.productParams.pageNumber = 0;
        this.productParams.pageSize = 0;
        this.getProducts();
    }

    reload(): void {
        this.productParams.searchString = "";
        this.productParams.pageNumber = 0;
        this.productParams.pageSize = 0;
        this.getProducts();
    }

    view(product?: any): void {
        const dialogRef = this.dialog.open(ProductViewComponent, {
            data: product
        });
        dialogRef.afterClosed().subscribe((result) => { });
    }

    getStockReport(): void {
        let orderParams: any = {
            productId: "",
            warehouseId: ""
        };

        this.reportService.getStockReport(orderParams).subscribe((result) => {
            result.data.forEach((x) => {
                var warehouse = this.warehouseLookups.find((obj) => {
                    return obj.id === x.warehouseId;
                });
                if (warehouse) {
                    x.warehouseName = warehouse.name;
                }
            });
            this.availableStock = result.data;
            this.stockReport = this.groupBy(result.data, (x) => x.productId);
            this.getProducts();
        });
    }

    groupBy(list, keyGetter) {
        const map = new Map();
        list.forEach((item) => {
            const key = keyGetter(item);
            let value = map.get(key);
            if (!value) {
                map.set(key, `${item.warehouseName} : ${item.availableQuantity} `);
            } else {
                map.set(key, `${value}  ${item.warehouseName} : ${item.availableQuantity} `);
            }
        });
        return map;
    }

    getAvailableStock() { }

    onExport($event) {
        let exportProductParams = new ProductParams();
        exportProductParams.pageNumber = 0;
        exportProductParams.pageSize = this.products.totalCount;
        this.productService.getProducts(exportProductParams).subscribe((result) => {
            result.data.forEach((p) => {
                var productStock = this.availableStock.filter((obj) => {
                    return obj.productId === p.id;
                });
                productStock.forEach((ps) => {
                    p[ps.warehouseName] = ps.availableQuantity;
                });
                delete p.localeName;
                delete p.categoryName;
                delete p.categoryId;
                delete p.taxMethod;
                delete p.tax;
                delete p.isAlert;
                delete p.alertQuantity;
                delete p.discountFactor;
                delete p.id;
                delete p.detail;
                delete p.brandId;
                delete p.brandName;
                delete p.warehouseId;
                delete p.openingStock;

            });
            this.csvParserService.exportXls(result.data, "products.xlsx", "Products");
        });
    }


    productsData: any[] = [];

    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridReady(event): void {
        if (this.AgGrid) {
            // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
        }
        // this.getOvertimeMyQueue();
        this.getProducts();
    }
    initOvertimeColumns(): void {
        this.productColumns = [
            { headerName: "Employee Name", field: "requestedForName", sortable: true, isShowable: true, width: 256 },
            {
                headerName: "Type", field: "overTimeType", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    if (value == 1) {
                        value = 'Hour'
                    } else {
                        value = 'Production'
                    }
                    return value;
                },
                width: 120
            },
            {
                headerName: "Ovetime Date", field: "attendanceDate", sortable: true, isShowable: true, valueFormatter: (params) => {
                    let value = params.value;
                    return value;
                },
                width: 150
            },
            {
                headerName: "In Time", field: "checkIn", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 2) {
                        return '-';
                    }
                    let value = params.value;
                   
                    return value;
                },
                width: 120
            },
            {
                headerName: "Out Time", field: "checkOut", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 2) {
                        return '-';
                    }
                    let value = params.value;
                   
                    return value;
                },
                width: 150
            },
            {
                headerName: "Production / Day", field: "production", sortable: true, valueFormatter: (params) => {
                    if (params && params.data && params.data.overTimeType == 1) {
                        return '-';
                    }
                    let value = params.value;
                    return value + ' / ' + params.data.requiredProduction;
                },
                width: 160
            },

            { headerName: "Hours", field: "overtimeHours", sortable: true, width: 160 },
            { headerName: "Status", field: "statusName", sortable: true, width: 160 },
            { headerName: "Comments", field: "reason", sortable: true, width: 330 },
            // {
            //     headerName: "Action",
            //     cellRenderer: "buttonRenderer",
            //     cellRendererParams: {
            //         buttons: ["Remove"],
            //         actionButtons: this.actionButtons,
            //         onClick: this.openDeleteConfirmationDialog.bind(this)
            //     },
            //     width: 50,
            //     pinned: "right"
            // }


        ];
    }
}

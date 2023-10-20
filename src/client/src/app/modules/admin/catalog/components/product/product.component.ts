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
            { headerName: "Shopify Id", field: "shopifyId", sortable: true, isShowable: true, width: 256 },
            { headerName: "Title", field: "title", sortable: true, isShowable: true, width: 256 },
            { headerName: "Vendor", field: "vendor", sortable: true, width: 160 },
            { headerName: "Product Type", field: "productType", sortable: true, width: 160 },
            { headerName: "PublishedScope", field: "publishedScope", sortable: true, width: 330 },
            { headerName: "Status", field: "status", sortable: true, width: 330 },
            { headerName: "Tags", field: "tags", sortable: true, width: 330 },
        ];
    }
}

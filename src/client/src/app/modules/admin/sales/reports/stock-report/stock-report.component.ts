import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { OrderStatus } from "src/app/core/enums/OrderStatus";
import { OrderType } from "src/app/core/enums/OrderType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { ProductParams } from "../../../catalog/models/productParams";
import { StockReport } from "../../../catalog/models/stockReport";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { StockInDetailComponent } from "../../components/stock/stock-in-detail/stock-in-detail.component";
import { Order } from "../../models/order";
import { OrderParams } from "../../models/orderParams";
import { PurchaseOrderService } from "../../services/purchase-order.service";
import { ReportService } from "../../services/report.service";
import { StockInService } from "../../services/stock-in.service";
import { WarehouseService } from "../../services/warehouse.service";

@Component({
    selector: "app-stock-report",
    templateUrl: "./stock-report.component.html",
    styleUrls: ["./stock-report.component.scss"]
})
export class StockReportComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams: any;
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
    ) {}

    ngOnInit(): void {
        this.loadLookups();
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
    getOrders(): void {
        this.reportService.getStockReport(this.orderParams).subscribe((result) => {
            var product = this.selectedProduct.productName.name;
            result.data.forEach((x) => {
                x.productName = product;
                var warehouse = this.warehouseLookups.find((obj) => {
                    return obj.id === x.warehouseId;
                });
                if (warehouse) {
                    x.warehouseName = warehouse.name;
                }
            });
            this.dataSource = result;
        });
    }

    pageChanged(event: PaginatedFilter): void {
        this.orderParams.pageNumber = event.pageNumber;
        this.orderParams.pageSize = event.pageSize;
        this.getOrders();
    }

    sort($event: Sort): void {
        this.orderParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.orderParams.orderBy);
        this.getOrders();
    }

    filter($event: string): void {
        this.orderParams.searchString = $event.trim().toLocaleLowerCase();
        this.orderParams.pageNumber = 0;
        this.orderParams.pageSize = 0;
        this.getOrders();
    }

    reload(): void {
        this.orderParams.searchString = "";
        this.orderParams.pageNumber = 0;
        this.orderParams.pageSize = 0;
        this.getOrders();
    }
    search(): void {
        // if (!this.selectedProduct.productName || typeof this.selectedProduct.productName !== "object") {
        //     this.toastr.error("Select valid product.");
        //     return;
        // }
        this.orderParams = {};
        this.orderParams.productId = this.selectedProduct.productName.id;
        this.orderParams.warehouseId = this.selectedProduct.warehouseId;
        this.getOrders();
    }
}

import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { ReportServiceApi } from "src/app/core/api/sales/report.service";
import { OrderStatus } from "src/app/core/enums/OrderStatus";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Result } from "src/app/core/models/wrappers/Result";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Product } from "../../../catalog/models/product";
import { VaraintReport } from "../../../catalog/models/variantReport";
import { DeleteDialogComponent } from "../../../shared/components/delete-dialog/delete-dialog.component";
import { PoDetailComponent } from "../../components/purchase-order/po-detail/po-detail.component";
import { Order } from "../../models/order";
import { OrderParams } from "../../models/orderParams";
import { PurchaseOrderService } from "../../services/purchase-order.service";
import { ReportService } from "../../services/report.service";
import { UpdatePromotionComponent } from "./update-promotion/update-promotion.component";
import * as XLSX from "XLSX";

@Component({
    selector: "app-variance-report",
    templateUrl: "./variance-report.component.html",
    styleUrls: ["./variance-report.component.scss"]
})
export class VarianceReportComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;

    displayedColumns: string[] = ["name", "productCode", "barcodeSymbology", "availableQuantity", "discountFactor", "promotionMode", "combinePricePkr", "afterDiscountPrices"];
    dataSource: Result<VaraintReport[]>;
    csvMapping: CsvMapping[];
    constructor(
        public purchaseOrderService: PurchaseOrderService,
        private route: Router,
        public dialog: MatDialog,
        public toastr: ToastrService,
        private csvParser: CsvParserService,
        private reportServiceApi: ReportService
    ) {}

    ngOnInit(): void {
        this.getOrders();
    }
    initCsvMapping() {
        this.csvMapping = [
            {
                csvColumn: "name",
                gridColumn: "name"
            },
            {
                csvColumn: "productCode",
                gridColumn: "productCode"
            },
            {
                csvColumn: "barcode",
                gridColumn: "barcode"
            },
            {
                csvColumn: "promotionMode",
                gridColumn: "promotionMode"
            },
            {
                csvColumn: "combinePricePkr",
                gridColumn: "combinePricePkr"
            },
            {
                csvColumn: "afterDiscountPrices",
                gridColumn: "afterDiscountPrices"
            }
        ];
    }
    getOrders(): void {
        this.purchaseOrderService.getOrders(this.orderParams).subscribe((result) => {
            // this.dataSource = result;
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
        this.orderParams.searchString = this.searchString.trim().toLocaleLowerCase();
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

    openViewOrderDetail(order: Order): void {
        const dialogRef = this.dialog.open(PoDetailComponent, {
            data: order
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
            }
        });
    }

    openEditPOS(orderId: string) {
        console.log(orderId);
        this.route.navigateByUrl("/admin/sales/po-edit/" + orderId);
    }

    openDeletePopup(orderId: string) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the delete of this purchase order?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.purchaseOrderService.deletePO(orderId).subscribe((res) => {
                    if (res.succeeded) {
                        this.toastr.success(res.messages[0]);
                        this.getOrders();
                    }
                });
            }
        });
    }

    isApproved(status: any) {
        return OrderStatus.Approved == status;
    }
    getStatus(status) {
        return OrderStatus[status];
    }
    arrayBuffer: any;
    filelist: any;
    file: File;
    handleFileSelect(event) {
        this.file = event.target.files[0];
        let fileReader = new FileReader();
        fileReader.readAsArrayBuffer(this.file);
        fileReader.onload = (e) => {
            this.arrayBuffer = fileReader.result;
            var data = new Uint8Array(this.arrayBuffer);
            var arr = new Array();
            for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
            var bstr = arr.join("");
            var workbook = XLSX.read(bstr, { type: "binary" });
            var first_sheet_name = workbook.SheetNames[0];
            var worksheet = workbook.Sheets[first_sheet_name];
            console.log(XLSX.utils.sheet_to_json(worksheet, { raw: true }));
            var arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: true });
            this.filelist = [];
            arraylist.forEach((obj) => {
                this.renameKey(obj, "Bar Code", "barcode");
                this.renameKey(obj, "Fine class (International) description", "description");
                this.renameKey(obj, "商品代码Product code", "productCode");
                this.renameKey(obj, "当地组合价（本地币）Combine price(PKR)", "combinePricePkr");
                this.renameKey(obj, "折后价 AFTER  DISCOUNT PRICES ", "afterDiscountPrices");
                this.renameKey(obj, "活动方式Promotion mode", "promotionMode");
                this.renameKey(obj, "Pack Size", "packSize");
                this.renameKey(obj, "Name", "name");
                obj["brandId"] = "370b8ad8-dfb1-45a4-a04d-e60d0ca97060";
                obj["categoryId"] = "c553786e-b8c5-4ff8-823d-120b35210a29";
            });
            console.log(arraylist);
            this.getProductVarianceReport(arraylist);
        };
    }
    renameKey(obj, oldKey, newKey) {
        obj[newKey] = obj[oldKey];
        delete obj[oldKey];
    }
    handleFileSelect1(evt) {
        var files = evt.target.files; // FileList object
        // Parse the file you want to select for the operation along with the configuration
        this.csvParser
            .parse(files[0], {
                header: true,
                delimiter: ",",
                mapping: this.csvMapping
            })
            .pipe()
            .subscribe(
                (result: Array<Product>) => {
                    console.log("Result", result);
                    let filteredResult = result.filter((x) => x.name != "");

                    filteredResult.forEach((element) => {
                        element.brandId = "370b8ad8-dfb1-45a4-a04d-e60d0ca97060";
                        element.categoryId = "c553786e-b8c5-4ff8-823d-120b35210a29";
                    });
                    // this.dataSource = <PaginatedResult<Product>>{};
                    // this.dataSource.data = filteredResult;
                    console.log(filteredResult);
                    this.getProductVarianceReport(filteredResult);
                },
                (error: NgxCSVParserError) => {
                    console.log("Error", error);
                }
            );
    }

    getProductVarianceReport(fileData) {
        //"6941447581124", "6941447592090", "6941447589700"
        let barcodes = [];
        fileData.forEach((element) => {
            barcodes.push("'" + element.productCode + "'");
        });
        var model = {
            Barcodes: barcodes
        };
        this.reportServiceApi.getVarianceReport(model).subscribe((res) => {
            console.log(res);

            fileData.forEach((element) => {
                res.data.forEach((p) => {
                    if (p.barcodeSymbology == element.barcode) {
                        p.productCode = element.productCode;
                        p.promotionMode = element.promotionMode;
                        p.combinePricePkr = element.combinePricePkr;
                        p.afterDiscountPrices = element.afterDiscountPrices;
                    }
                });
            });
            this.dataSource = res;
        });
    }

    openForm(): void {
        const dialogRef = this.dialog.open(UpdatePromotionComponent, {
            data: this.dataSource.data
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
            }
        });
    }
}

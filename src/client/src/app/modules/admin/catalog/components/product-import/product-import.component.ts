import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Product } from "../../models/product";
import { ProductParams } from "../../models/productParams";
import { ProductService } from "../../services/product.service";
import { ProductFormComponent } from "../product/product-form/product-form.component";

@Component({
    selector: "app-product-import",
    templateUrl: "./product-import.component.html",
    styleUrls: ["./product-import.component.scss"]
})
export class ProductImportComponent implements OnInit {
    products: PaginatedResult<Product>;
    productColumns: TableColumn[];
    productParams = new ProductParams();
    searchString: string;
    csvMapping: CsvMapping[];

    constructor(public productService: ProductService, public dialog: MatDialog, public toastr: ToastrService, private csvParser: CsvParserService) {}

    ngOnInit(): void {
        this.products = <PaginatedResult<Product>>{
            currentPage: 0,
            data: [],
            pageSize: 5,
            totalCount: 0,
            totalPages: 0
        };
        this.initCsvMapping();
        this.initColumns();
    }

    getProducts(): void {
        this.productService.getProducts(this.productParams).subscribe((result) => {
            this.products = result;
        });
    }

    initColumns(): void {
        this.productColumns = [
            { name: "Name", dataKey: "name", isSortable: true, isShowable: true },
            {
                name: "Description",
                dataKey: "detail",
                isSortable: true,
                isShowable: true
            },
            {
                name: "productCode",
                dataKey: "productCode",
                isSortable: true,
                isShowable: true
            },
            {
                name: "barcode",
                dataKey: "barcodeSymbology",
                isSortable: true,
                isShowable: true
            },
            { name: "status", dataKey: "status", isSortable: true, isShowable: true },
            {
                name: "quantity",
                dataKey: "openingStock",
                isSortable: true,
                isShowable: true
            },
            {
                name: "WarehouseId",
                dataKey: "warehouseId",
                isSortable: true,
                isShowable: true
            },
            {
                name: "location",
                dataKey: "location",
                isSortable: true,
                isShowable: true
            },
            { name: "price", dataKey: "price", isSortable: true, isShowable: true },
            {
                name: "discountFactor",
                dataKey: "discountFactor",
                isSortable: false,
                isShowable: false
            },
            {
                name: "imageUrl",
                dataKey: "imageUrl",
                isSortable: true,
                isShowable: true
            }
            //{ name: 'IsAlert', dataKey: 'isAlert', isSortable: true, isShowable: true },
            // { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    initCsvMapping() {
        this.csvMapping = [
            {
                csvColumn: "Title",
                gridColumn: "name"
            },
            {
                csvColumn: "Description",
                gridColumn: "detail"
            },
            {
                csvColumn: "product code",
                gridColumn: "productCode"
            },
            {
                csvColumn: "Variant Barcode",
                gridColumn: "barcodeSymbology"
            },
            {
                csvColumn: "Status",
                gridColumn: "status"
            },
            {
                csvColumn: "Variant Inventory Qty",
                gridColumn: "openingStock"
            },
            {
                csvColumn: "Variant SKU",
                gridColumn: "location"
            },
            {
                csvColumn: "Variant Price",
                gridColumn: "price"
            },
            {
                csvColumn: "discountFactor",
                gridColumn: "discountFactor"
            },
            {
                csvColumn: "Image Src",
                gridColumn: "imageUrl"
            },
            {
                csvColumn: "WarehouseId",
                gridColumn: "warehouseId"
            }
        ];
    }
    pageChanged(event: PaginatedFilter): void {
        this.productParams.pageNumber = event.pageNumber;
        this.productParams.pageSize = event.pageSize;
        this.getProducts();
    }

    openForm(product?: Product): void {
        // const dialogRef = this.dialog.open(ProductFormComponent, {
        //   data: product,
        // });
        // dialogRef.afterClosed().subscribe((result) => {
        //   if (result) {
        //     this.getProducts();
        //   }
        // });
        this.onSubmit();
    }

    remove($event: string): void {
        // this.productService.deleteProduct($event).subscribe(() => {
        //   this.getProducts();
        //   this.toastr.info('Product Removed');
        // });
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
    csvRecords: any[] = [];
    handleFileSelect(evt) {
        var files = evt.target.files; // FileList object
        // Parse the file you want to select for the operation along with the configuration
        this.csvParser
            .parseXlsx(files[0], {
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

                    this.products = <PaginatedResult<Product>>{
                        currentPage: 0,
                        data: filteredResult,
                        pageSize: 5,
                        totalCount: filteredResult.length,
                        totalPages: filteredResult.length / 10
                    };
                },
                (error: NgxCSVParserError) => {
                    console.log("Error", error);
                }
            );
    }

    onSubmit() {
        // TODO after successful update/insert, refresh table view in component product.component.ts

        if (this.products.data.length > 0) {
            var model = {
                products: this.products.data
            };
            this.productService.importProduct(model).subscribe((response) => {
                if (response.succeeded) {
                    this.toastr.success(response.messages[0]);
                    this.products = <PaginatedResult<Product>>{
                        currentPage: 0,
                        data: [],
                        pageSize: 5,
                        totalCount: 0,
                        totalPages: 0
                    };
                } else {
                    this.toastr.error(response.messages[0]);
                }
            });
        }
    }
}

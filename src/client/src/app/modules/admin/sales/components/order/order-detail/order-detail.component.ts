import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ProductApiService } from "src/app/core/api/catalog/product-api.service";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Product } from "src/app/modules/admin/catalog/models/product";
import { ProductParams } from "src/app/modules/admin/catalog/models/productParams";
import { ProductService } from "src/app/modules/admin/catalog/services/product.service";
import { Order } from "../../../models/order";
import { SalesService } from "../../../services/sales.service";

@Component({
    selector: "app-order-detail",
    templateUrl: "./order-detail.component.html",
    styleUrls: ["./order-detail.component.scss"]
})
export class OrderDetailComponent implements OnInit {
    order: Order;
    products: Product[];
    displayedColumns: string[] = ["barcodeSymbology", "productName", "quantity", "price", "total"];
    constructor(@Inject(MAT_DIALOG_DATA) public data: Order, private toastr: ToastrService, private saleService: SalesService, private productApi: ProductService) {}

    ngOnInit(): void {
        this.getProducts();
    }
    getOrder() {
        this.saleService.getById(this.data.id).subscribe((response) => {
            response.data.products.forEach((x) => {
                x.productName = this.getProductName(x.productId);
                x.barcodeSymbology = this.getProductBarcode(x.productId);
            });
            this.order = response.data;
        });
    }
    getProducts() {
        let params = new ProductParams();
        params.pageSize = 10000;
        this.productApi.getProducts(params).subscribe((res) => {
            this.products = res.data;
            this.getOrder();
        });
    }

    getProductName(id) {
        console.log(this.products);
        let product = this.products.find((x) => x.id == id);
        if (product) {
            return product.name;
        }
        return "";
    }

    getProductBarcode(id) {
        console.log(this.products);
        let product = this.products.find((x) => x.id == id);
        if (product) {
            return product.barcodeSymbology;
        }
        return "";
    }
}

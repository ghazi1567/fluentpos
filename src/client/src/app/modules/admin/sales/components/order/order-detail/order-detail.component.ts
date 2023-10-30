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
import { OrderStatusMapping } from "src/app/core/enums/OrderStatus";
import { ActivatedRoute } from "@angular/router";
import { OrdersService } from "../../../services/orders.service";

@Component({
    selector: "app-order-detail",
    templateUrl: "./order-detail.component.html",
    styleUrls: ["./order-detail.component.scss"]
})
export class OrderDetailComponent implements OnInit {
    orderId: any;
    order: any;
    products: Product[];
    displayedColumns: string[] = ["barcodeSymbology", "productName", "quantity", "price", "total"];
    public OrderStatusMapping = OrderStatusMapping;
    constructor(private toastr: ToastrService,
        private orderService: OrdersService,
        private productApi: ProductService,
        private route: ActivatedRoute) {
        this.orderId = this.route.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.getOrder();
    }

    getOrder() {
        this.orderService.getById(this.orderId).subscribe((response) => {
            this.order = response.data;
        });
    }

    cancelOrder() {
        var model = {
            id: this.order.id,
            shopifyId: this.order.shopifyId,
            reason: ''
        }
        this.orderService.cancelOrder(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
            } else {

            }
        },
            error => {
                console.log('oops', error)
                this.toastr.error(error);
            }
        );
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

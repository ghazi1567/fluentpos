import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
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
import { WarehouseService } from "../../../services/warehouse.service";
import { SplitOrderComponent } from "../split-order/split-order.component";

@Component({
    selector: "app-order-detail",
    templateUrl: "./order-detail.component.html",
    styleUrls: ["./order-detail.component.scss"]
})
export class OrderDetailComponent implements OnInit {
    orderId: any;
    order: any;
    warehouseData: any[];
    products: Product[];
    displayedColumns: string[] = ["barcodeSymbology", "productName", "quantity", "price", "total"];
    public OrderStatusMapping = OrderStatusMapping;
    constructor(private toastr: ToastrService,
        private orderService: OrdersService,
        private productApi: ProductService,
        private warehouseService: WarehouseService,
        public dialog: MatDialog,
        private route: ActivatedRoute) {
        this.orderId = this.route.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.getWarehouses();
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

    fulFillOrder(fulFillOrder: any) {
        var model = {
            id: this.order.id,
            shopifyId: this.order.shopifyId,
            fulFillOrderId: fulFillOrder.shopifyId,
            reason: ''
        };

        this.orderService.fulFillOrder(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
            }
        },
            error => {
                console.log('oops', error)
                this.toastr.error(error);
            }
        );
    }

    approveOrder() {
        var model = {
            id: this.order.id,
            shopifyId: this.order.shopifyId,
            reason: ''
        }
        this.orderService.approveOrder(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.getOrder();
            } else {

            }
        },
            error => {
                console.log('oops', error)
                this.toastr.error(error);
            }
        );
    }

    splitOrderPopup() {
        const dialogRef = this.dialog.open(SplitOrderComponent, {
            data: this.order
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {

            }
        });
    }

    getLineItemDetail(item: any, field: string) {
        var lineItem = this.order.lineItems.find(x => x.variant_id == item.variant_id)
        return lineItem[field];
    }

    getWarehouses() {
        this.warehouseService.getAll().subscribe(res => {
            this.warehouseData = res.data;
        })
    }

    getLocationName(locationId) {
        var location = this.warehouseData.find(x => x.shopifyId == locationId)
        return location.name;
    }
}

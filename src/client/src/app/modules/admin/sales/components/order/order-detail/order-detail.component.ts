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
import { OrderStatus, OrderStatusMapping } from "src/app/core/enums/OrderStatus";
import { ActivatedRoute, Router } from "@angular/router";
import { OrdersService } from "../../../services/orders.service";
import { WarehouseService } from "../../../services/warehouse.service";
import { SplitOrderComponent } from "../split-order/split-order.component";
import { DeleteDialogComponent } from "src/app/modules/admin/shared/components/delete-dialog/delete-dialog.component";

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
    constructor(
        private toastr: ToastrService,
        private orderService: OrdersService,
        private productApi: ProductService,
        private warehouseService: WarehouseService,
        public dialog: MatDialog,
        private route: ActivatedRoute,
        public router: Router
    ) {
        this.orderId = this.route.snapshot.params["id"];
    }

    ngOnInit(): void {
        this.getWarehouses();
        this.getOrder();
    }

    getOrder() {
        this.orderService.getFOById(this.orderId).subscribe((response) => {
            this.order = response.data;
        });
    }

    displayButton(status, button) {
        switch (button) {
            case "approve":
                return OrderStatus.Pending == status || OrderStatus.PendingApproval == status || OrderStatus.WAConfirmation == status || OrderStatus.WAFailed == status;
                break;
            case "cancel":
                return OrderStatus.Shipped != status && OrderStatus.Cancelled != status;
                break;
            case "acceptOrder":
                return OrderStatus.AssignToOutlet == status;
                break;
            case "rejectOrder":
                return OrderStatus.AssignToOutlet == status;
                break;
            case "confirmOrder":
                return OrderStatus.Preparing == status;
                break;
            case "reQueueOrder":
                return OrderStatus.AssignToHeadOffice == status || OrderStatus.ReQueueAfterReject == status;
                break;

            default:
                break;
        }
        return false;
    }

    cancelOrder(reason) {
        var model = {
            id: this.order.id,
            shopifyId: this.order.shopifyId,
            reason: reason,
            fulfillmentOrderId: this.order.fulFillmentOrderId
        };
        this.orderService.cancelOrder(model).subscribe(
            (res) => {
                if (res.succeeded) {
                    this.toastr.success(res.messages[0]);
                } else {
                }
            },
            (error) => {
                console.log("oops", error);
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
            reason: ""
        };

        this.orderService.fulFillOrder(model).subscribe(
            (res) => {
                if (res.succeeded) {
                    this.toastr.success(res.messages[0]);
                }
            },
            (error) => {
                console.log("oops", error);
                this.toastr.error(error);
            }
        );
    }

    approveOrder(comments) {
        var model = {
            id: this.order.id,
            shopifyId: this.order.shopifyId,
            reason: comments,
            fulFillOrderId: this.order.internalFulFillmentOrderId
        };
        this.orderService.approveOrder(model).subscribe(
            (res) => {
                if (res.succeeded) {
                    this.toastr.success(res.messages[0]);
                    this.getOrder();
                } else {
                }
            },
            (error) => {
                console.log("oops", error);
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
        var lineItem = this.order.lineItems.find((x) => x.variant_id == item.variantId);
        return lineItem[field];
    }

    getWarehouses() {
        this.warehouseService.getAll().subscribe((res) => {
            this.warehouseData = res.data;
        });
    }

    getLocationName(locationId) {
        if (!this.warehouseData) {
            return "";
        }
        var location = this.warehouseData.find((x) => x.shopifyId == locationId);
        return location.name;
    }

    acceptOrder() {
        var model = {
            id: this.order.id,
            ShopifyId: this.order.ShopifyId,
            FulfillmentOrderId: this.order.fulFillmentOrderId
        };
        this.orderService.acceptOrder(model).subscribe(
            (res) => {
                if (res.succeeded) {
                    this.toastr.success(res.messages[0]);
                    this.getOrder();
                } else {
                }
            },
            (error) => {
                console.log("oops", error);
                this.toastr.error(error);
            }
        );
    }
    rejectOrder(comments) {
        var model = {
            id: this.order.id,
            ShopifyId: this.order.ShopifyId,
            FulfillmentOrderId: this.order.fulFillmentOrderId
        };
        this.orderService.requeueOrder(model).subscribe(
            (res) => {
                if (res.succeeded) {
                    this.toastr.success(res.messages[0]);
                    this.router.navigateByUrl(`admin/sales/orders`);
                }
            },
            (error) => {
                console.log("oops", error);
                this.toastr.error(error);
            }
        );
    }
    openConfirmationDialog(button: string, showComments) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: {
                message: `Are you sure, you want ${button} this order?`,
                showComments: showComments || false,
                commentLabel: "Comments",
                confirmButtonLabel: button,
                cancelButtonLabel: "Back",
                commentRequired: false,
                confirmColor: "primary",
                event: button
            }
        });

        dialogRef.afterClosed().subscribe((result) => {
            console.log(result);
            if (result.confirmed == true) {
                switch (button) {
                    case "approve":
                        this.approveOrder(result.comments);
                        break;
                    case "cancel":
                        this.cancelOrder(result.comments);
                        break;
                    case "accept":
                        this.acceptOrder();
                        break;
                    case "reject":
                        this.rejectOrder(result.comments);
                        break;
                    case "reQueue":
                        this.rejectOrder(result.comments);
                        break;
                    default:
                        break;
                }
            }
        });
    }
}

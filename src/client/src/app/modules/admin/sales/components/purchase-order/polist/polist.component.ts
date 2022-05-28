import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { OrderStatus } from "src/app/core/enums/OrderStatus";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { DeleteDialogComponent } from "src/app/modules/admin/shared/components/delete-dialog/delete-dialog.component";
import { Order } from "../../../models/order";
import { OrderParams } from "../../../models/orderParams";
import { PurchaseOrderService } from "../../../services/purchase-order.service";
import { SalesService } from "../../../services/sales.service";
import { OrderDetailComponent } from "../../order/order-detail/order-detail.component";
import { PoDetailComponent } from "../po-detail/po-detail.component";

@Component({
    selector: "app-polist",
    templateUrl: "./polist.component.html",
    styleUrls: ["./polist.component.scss"]
})
export class POListComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;

    displayedColumns: string[] = ["id", "referenceNumber", "timeStamp", "note", "status", "action"];
    dataSource: PaginatedResult<Order>;

    constructor(public purchaseOrderService: PurchaseOrderService, 
        private route: Router,
        public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getOrders();
    }

    getOrders(): void {
        this.purchaseOrderService.getOrders(this.orderParams).subscribe((result) => {
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
        this.route.navigateByUrl('/admin/sales/po-edit/' + orderId);
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
}

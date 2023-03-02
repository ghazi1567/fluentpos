import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { OrderStatus } from "src/app/core/enums/OrderStatus";
import { OrderType } from "src/app/core/enums/OrderType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { DeleteDialogComponent } from "src/app/modules/admin/shared/components/delete-dialog/delete-dialog.component";
import { Order } from "../../../models/order";
import { OrderParams } from "../../../models/orderParams";
import { StockInService } from "../../../services/stock-in.service";
import { StockInDetailComponent } from "../stock-in-detail/stock-in-detail.component";

@Component({
    selector: "app-approval-list",
    templateUrl: "./approval-list.component.html",
    styleUrls: ["./approval-list.component.scss"]
})
export class ApprovalListComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;

    displayedColumns: string[] = ["id", "poReferenceNo", "referenceNumber", "timeStamp", "orderType", "action"];
    dataSource: PaginatedResult<Order>;

    constructor(public stockInService: StockInService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getOrders();
    }

    getOrders(): void {
        this.orderParams.orderType = OrderType.StockIn;
        this.orderParams.status = OrderStatus.PendingApproval;
        this.stockInService.getStockInOrders(this.orderParams).subscribe((result) => {
            this.dataSource = result;
        });
    }
    getOrderType(orderType) {
        return OrderType[orderType];
    }
    getStatus(status) {
        return OrderStatus[status];
    }
    pageChanged(event: PaginatedFilter): void {
        this.orderParams.pageNumber = event.pageIndex + 1;
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

    openViewOrderDetail(order: Order): void {
        const dialogRef = this.dialog.open(StockInDetailComponent, {
            data: order
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
            }
        });
    }

    openEditPOS(orderId: string) {
        console.log(orderId);
    }

    openApprovalConfirmationDialog(id: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the approval of this Stock in?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                let model = {
                    orderId: id,
                    status: OrderStatus.Approved
                };
                this.stockInService.approve(model).subscribe((res) => {
                    if (res.succeeded) {
                        this.toastr.success(res.messages[0]);
                    }
                    this.getOrders();
                });
            }
        });
    }

    openRejectConfirmationDialog(id: any) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the reject of this Stock in?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                let model = {
                    orderId: id,
                    status: OrderStatus.Rejected
                };
                this.stockInService.approve(model).subscribe((res) => {
                    if (res.succeeded) {
                        this.toastr.success(res.messages[0]);
                    }
                    this.getOrders();
                });
            }
        });
    }
}

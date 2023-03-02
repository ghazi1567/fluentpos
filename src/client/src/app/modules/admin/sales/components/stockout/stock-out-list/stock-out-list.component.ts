import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { OrderStatus } from "src/app/core/enums/OrderStatus";
import { OrderType } from "src/app/core/enums/OrderType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { DeleteDialogComponent } from "src/app/core/shared/components/delete-dialog/delete-dialog.component";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Order } from "../../../models/order";
import { OrderParams } from "../../../models/orderParams";
import { StockInService } from "../../../services/stock-in.service";
import { StockOutService } from "../../../services/stock-out.service";
import { StockInDetailComponent } from "../../stock/stock-in-detail/stock-in-detail.component";
import { StockOutDetailComponent } from "../stock-out-detail/stock-out-detail.component";

@Component({
    selector: "app-stock-out-list",
    templateUrl: "./stock-out-list.component.html",
    styleUrls: ["./stock-out-list.component.scss"]
})
export class StockOutListComponent implements OnInit {
    orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;

    displayedColumns: string[] = ["id", "referenceNumber", "note", "timeStamp", "orderType", "action"];
    dataSource: PaginatedResult<Order>;

    constructor(public stockInService: StockOutService, private route: Router, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getOrders();
    }

    getOrders(): void {
        this.orderParams.orderType = OrderType.StockOut;
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

    isApproved(status: any) {
        return OrderStatus.Approved == status;
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
        this.orderParams.searchString = this.searchString;
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
        const dialogRef = this.dialog.open(StockOutDetailComponent, {
            data: order
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
            }
        });
    }

    openEditPOS(orderId: string) {
        this.route.navigateByUrl("/admin/sales/stock-out-edit/" + orderId);
    }
    openDeletePopup(orderId: string) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the delete of this stock in?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.stockInService.delete(orderId).subscribe((res) => {
                    if (res.succeeded) {
                        this.toastr.success(res.messages[0]);
                        this.getOrders();
                    }
                });
            }
        });
    }
}

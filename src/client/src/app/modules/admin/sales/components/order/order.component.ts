import { OrderDetailComponent } from "./order-detail/order-detail.component";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Order } from "../../models/order";
import { OrderParams } from "../../models/orderParams";
import { ToastrService } from "ngx-toastr";
import { Sort } from "@angular/material/sort";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SalesService } from "../../services/sales.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { OrderStatus, OrderStatusMapping } from "src/app/core/enums/OrderStatus";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { Router } from "@angular/router";
import * as moment from "moment";
import { Observable, of } from "rxjs";
import { RemoteGridApi } from "src/app/core/shared/components/ag-grid-base/ag-grid.models";
import { GridApi } from "ag-grid-community";
import { map } from "rxjs/operators";
import { OrdersService } from "../../services/orders.service";

@Component({
    selector: "app-order",
    templateUrl: "./order.component.html",
    styleUrls: ["./order.component.scss"]
})
//RemoteGridApi
export class OrderComponent implements OnInit {
    // orderColumns: TableColumn[];
    orderParams = new OrderParams();
    searchString: string;
    orderData: any[] = [];
    pendingOrderData: any[] = [];

    approvedOrderData: any[] = [];
    cancelledOrderData: any[] = [];
    assignedToOutletOrderData: any[] = [];
    preparingOrderCount: number = 0;
    rtsOrderCount: number = 0;
    shippedOrderCount: number = 0;
    athoOrderCount: number = 0;
    atolOrderCount: number = 0;

    displayedColumns: string[] = ["id", "referenceNumber", "timeStamp", "customerName", "total", "isPaid", "action"];
    dataSource: PaginatedResult<Order>;

    constructor(public ordersService: OrdersService, public saleService: SalesService, public dialog: MatDialog, public toastr: ToastrService, public router: Router) {}
    getDataError(err: any) {
        console.log(err);
    }
    gridApi: GridApi;

    ngOnInit(): void {
        // this.getOrders();
        this.initOvertimeColumns();
        // this.syncOrders();
    }

    getOrders(status: number): void {
        this.orderParams.pageNumber = 0;
        this.orderParams.pageSize = 100;
        this.orderParams.status = status;

        this.saleService.getSales(this.orderParams).subscribe((result) => {
            this.dataSource = result;
            this.orderData = result.data;
        });
    }

    openViewOrderDetail(order: Order): void {
        const dialogRef = this.dialog.open(OrderDetailComponent, {
            data: order
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
            }
        });
    }

    public OrderStatusMapping = OrderStatusMapping;

    orderColumns: any[];
    private AgGrid: AgGridBaseComponent;
    remoteGridBinding = this;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridReady(event): void {
        this.gridApi = event.params.api;
        // event.api.sizeColumnsToFit();
        if (this.AgGrid) {
            // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
        }
        // this.getOvertimeMyQueue();
        this.getOrders(null);
    }

    actionButtons: CustomAction[] = [new CustomAction("View Order", "View", "View", "visibility")];
    initOvertimeColumns(): void {
        this.orderColumns = [
            {
                headerName: "",
                cellRenderer: "buttonRenderer",
                filter: false,
                sortable: false,
                cellRendererParams: {
                    buttons: ["View"],
                    actionButtons: this.actionButtons,
                    onClick: this.onButtonClick.bind(this)
                },
                width: 60,
                pinned: "left",
                resizable: false
            },
            { headerName: "Shopify Id", field: "shopifyId", sortable: true, isShowable: true, width: 140 },
            { headerName: "Order#", field: "name", sortable: true, isShowable: true, width: 120 },
            {
                headerName: "Status",
                field: "status",
                sortable: true,
                isShowable: true,
                width: 140,
                wrapText: true,
                autoHeight: true,
                valueGetter: (params) => {
                    return params.data ? OrderStatusMapping[params.data.status] : "";
                }
            },
            {
                headerName: "Warehouse",
                field: "warehouseName",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true,
                width: 140
            },
            {
                headerName: "First Name",
                field: "firstName",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true,
                width: 120
            },
            {
                headerName: "Last Name",
                field: "lastName",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true,
                width: 120
            },
            {
                headerName: "City",
                field: "city",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true,
                width: 100
            },
            {
                headerName: "Country",
                field: "country",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true,
                width: 110
            },
            {
                headerName: "Phone",
                field: "phone",
                sortable: true,
                isShowable: true,
                width: 140
            },
            {
                headerName: "Email",
                field: "email",
                sortable: true,
                isShowable: true,
                wrapText: true,
                autoHeight: true
            },

            { headerName: "Note", field: "note", sortable: true, isShowable: true },
            // { headerName: "Confirmed", field: "confirmed", sortable: true, isShowable: true, },
            {
                headerName: "Created At",
                field: "createdAt",
                sortable: true,
                isShowable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "DD-MM-YYYY hh:mm:ss");
                    if (date.isValid()) {
                        value = date.format("DD-MM-YYYY hh:mm:ss");
                    }
                    return value;
                }
            },
            // { headerName: "Currency", field: "currency", sortable: true, isShowable: true, },
            // { headerName: "CustomerLocale", field: "customerLocale", sortable: true, isShowable: true, },
            // { headerName: "Email", field: "email", sortable: true, isShowable: true, },
            { headerName: "TrackingCompany", field: "trackingCompany", sortable: true, isShowable: true },
            { headerName: "TrackingNumber", field: "trackingNumber", sortable: true, isShowable: true },
            { headerName: "TrackingStatus", field: "trackingStatus", sortable: true, isShowable: true },
            // { headerName: "Phone", field: "phone", sortable: true, isShowable: true, },
            // { headerName: "Tags", field: "tags", sortable: true, isShowable: true, },
            // { headerName: "LocationId", field: "locationId", sortable: true, isShowable: true, },
            // { headerName: "OrderNumber", field: "orderNumber", sortable: true, isShowable: true, },
            {
                headerName: "Payment Method",
                field: "paymentGatewayNames",
                sortable: true,
                isShowable: true
            },
            {
                headerName: "ProcessedAt",
                field: "processedAt",
                sortable: true,
                isShowable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "DD-MM-YYYY hh:mm:ss");
                    if (date.isValid()) {
                        value = date.format("DD-MM-YYYY hh:mm:ss");
                    }
                    return value;
                }
            },
            { headerName: "ProcessingMethod", field: "processingMethod", sortable: true, isShowable: true },
            // { headerName: "ShippingAddress", field: "shippingAddress", sortable: true, isShowable: true, },
            { headerName: "SubtotalPrice", field: "subtotalPrice", sortable: true, isShowable: true },
            { headerName: "TaxesIncluded", field: "taxesIncluded", sortable: true, isShowable: true },
            { headerName: "TotalDiscounts", field: "totalDiscounts", sortable: true, isShowable: true },
            { headerName: "TotalLineItemsPrice", field: "totalLineItemsPrice", sortable: true, isShowable: true },
            { headerName: "TotalTipReceived", field: "totalTipReceived", sortable: true, isShowable: true },
            { headerName: "TotalPrice", field: "totalPrice", sortable: true, isShowable: true },
            { headerName: "TotalTax", field: "totalTax", sortable: true, isShowable: true },
            { headerName: "TotalWeight", field: "totalWeight", sortable: true, isShowable: true },
            { headerName: "UserId", field: "userId", sortable: true, isShowable: true },
            { headerName: "PresentmentCurrency", field: "presentmentCurrency", sortable: true, isShowable: true },
            { headerName: "TotalOutstanding", field: "totalOutstanding", sortable: true, isShowable: true },
            { headerName: "EstimatedTaxes", field: "estimatedTaxes", sortable: true, isShowable: true },
            { headerName: "CurrentSubtotalPrice", field: "currentSubtotalPrice", sortable: true, isShowable: true },
            { headerName: "CurrentTotalDiscounts", field: "currentTotalDiscounts", sortable: true, isShowable: true },
            { headerName: "CurrentTotalPrice", field: "currentTotalPrice", sortable: true, isShowable: true },
            { headerName: "CurrentTotalTax", field: "currentTotalTax", sortable: true, isShowable: true },
            { headerName: "TaxExempt", field: "taxExempt", sortable: true, isShowable: true },
            { headerName: "CancelReason", field: "cancelReason", sortable: true, isShowable: true },
            {
                headerName: "CancelledAt",
                field: "cancelledAt",
                sortable: true,
                isShowable: true,
                valueFormatter: (params) => {
                    if (params && params.value) {
                        let value = params.value;
                        let date = moment(value, "DD-MM-YYYY hh:mm:ss");
                        if (date.isValid()) {
                            value = date.format("DD-MM-YYYY hh:mm:ss");
                        }
                        return value;
                    }
                    return "";
                }
            }
        ];
    }

    syncOrders() {
        this.saleService.syncOrders().subscribe((result) => {
            this.toastr.success("Job triggered successfully.");
        });
    }

    // getData(params): Observable<{ data; totalCount }> {
    //   console.log(params);
    //   this.orderParams.pageNumber = params.startRow;
    //   this.orderParams.pageSize = params.endRow;
    //   return this.saleService.getSales(this.orderParams)
    // }

    tabSelection($event) {
        console.log($event);
        if ($event.tabId == "1") {
            this.pendingOrderData = this.orderData.filter((x) => x.status == 1);
        } else if ($event.tabId == "5") {
            this.approvedOrderData = this.orderData.filter((x) => x.status == 5);
        } else if ($event.tabId == "2") {
            this.cancelledOrderData = this.orderData.filter((x) => x.status == 2);
        } else if ($event.tabId == "6") {
            this.assignedToOutletOrderData = this.orderData.filter((x) => x.status == 6);
        }
    }

    onButtonClick(params) {
        console.log(params);
        if (params.button.key == "View") {
            this.router.navigateByUrl(`admin/sales/order-detail/${params.data.internalFulFillmentOrderId}`);
        }
        if (params.button.key == "RequeueOrder") {
            this.requeueOrders(params.data);
        }
    }
    requeueOrders(order) {
        var model = {
            id: order.id,
            ShopifyId: order.shopifyId,
            FulfillmentOrderId: order.fulFillmentOrderId
        };
        this.ordersService.requeueOrder(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
            } else {
                this.toastr.error(res.messages[0]);
            }
        });
    }
}

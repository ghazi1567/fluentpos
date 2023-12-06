import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { GridApi } from 'ag-grid-community';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { OrderStatusMapping, OrderStatus } from 'src/app/core/enums/OrderStatus';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { CustomAction } from 'src/app/core/shared/components/table/custom-action';
import { Order } from '../../../models/order';
import { OrderParams } from '../../../models/orderParams';
import { OrdersService } from '../../../services/orders.service';
import { SalesService } from '../../../services/sales.service';
import { OrderDetailComponent } from '../order-detail/order-detail.component';

@Component({
  selector: 'app-assigned-to-headoffice',
  templateUrl: './assigned-to-headoffice.component.html',
  styleUrls: ['./assigned-to-headoffice.component.scss']
})
export class AssignedToHeadofficeComponent implements OnInit {
  // orderColumns: TableColumn[];
  @Input() count: number;
  @Output() countChange = new EventEmitter<number>();

  setCount(count): void {
    this.countChange.emit(count);
  }

  orderParams = new OrderParams();
  searchString: string;
  orderData: any[] = [];

  displayedColumns: string[] = ['id', 'referenceNumber', 'timeStamp', 'customerName', 'total', 'isPaid', 'action'];
  dataSource: PaginatedResult<Order>;

  constructor(
    public saleService: SalesService,
    private orderService: OrdersService,
    public dialog: MatDialog,
    public toastr: ToastrService,
    public router: Router
  ) { }
  getDataError(err: any) {
    console.log(err)
  };
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
      this.setCount(result.totalCount);
    });

  }

  openViewOrderDetail(order: Order): void {
    const dialogRef = this.dialog.open(OrderDetailComponent, {
      data: order,
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
    this.getOrders(OrderStatus.AssignToHeadOffice);
  }

  actionButtons: CustomAction[] = [
    new CustomAction("View Order", "View", "View", "visibility"),
  ];
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
        pinned: "left",
        width: 80
      },
      { headerName: "Shopify Id", field: "shopifyId", sortable: true, isShowable: true, width: 140 },
      { headerName: "Order#", field: "name", sortable: true, isShowable: true, width: 100 },
      {
        headerName: "Status", field: "status", sortable: true, isShowable: true, width: 140,
        wrapText: true,
        autoHeight: true,
        valueGetter: params => {
          return params.data ? OrderStatusMapping[params.data.status] : '';
        }
      },
      {
        headerName: "Warehouse", field: "warehouseName", sortable: true, isShowable: true,
        wrapText: true,
        autoHeight: true,
      },
      {
        headerName: "Cust. Name", field: "customerName", sortable: true, isShowable: true,
        wrapText: true,
        autoHeight: true,
        valueGetter: 'data.shippingAddress.name'
      },
      {
        headerName: "Cust. Phone", field: "phone", sortable: true, isShowable: true, width: 140,
        valueGetter: 'data.shippingAddress.phone'
      },
      {
        headerName: "Cust. Email", field: "email", sortable: true, isShowable: true,
        wrapText: true,
        autoHeight: true,
      },

      { headerName: "Note", field: "note", sortable: true, isShowable: true, },
      // { headerName: "Confirmed", field: "confirmed", sortable: true, isShowable: true, },
      {
        headerName: "CreatedAt", field: "createdAt", sortable: true, isShowable: true,
        valueFormatter: (params) => {
          let value = params.value;
          let date = moment(value, "DD-MM-YYYY hh:mm:ss");
          if (date.isValid()) {
            value = date.format("DD-MM-YYYY hh:mm:ss");
          }
          return value;
        }
      },
      { headerName: "Currency", field: "currency", sortable: true, isShowable: true, },
      // { headerName: "CustomerLocale", field: "customerLocale", sortable: true, isShowable: true, },
      // { headerName: "Email", field: "email", sortable: true, isShowable: true, },
      { headerName: "FinancialStatus", field: "financialStatus", sortable: true, isShowable: true, },
      { headerName: "FulfillmentStatus", field: "fulfillmentStatus", sortable: true, isShowable: true, },
      // { headerName: "Phone", field: "phone", sortable: true, isShowable: true, },
      // { headerName: "Tags", field: "tags", sortable: true, isShowable: true, },
      { headerName: "LocationId", field: "locationId", sortable: true, isShowable: true, },
      // { headerName: "OrderNumber", field: "orderNumber", sortable: true, isShowable: true, },
      {
        headerName: "Payment Method", field: "paymentGatewayNames", sortable: true, isShowable: true,
      },
      {
        headerName: "ProcessedAt", field: "processedAt", sortable: true, isShowable: true,
        valueFormatter: (params) => {
          let value = params.value;
          let date = moment(value, "DD-MM-YYYY hh:mm:ss");
          if (date.isValid()) {
            value = date.format("DD-MM-YYYY hh:mm:ss");
          }
          return value;
        }
      },
      { headerName: "ProcessingMethod", field: "processingMethod", sortable: true, isShowable: true, },
      // { headerName: "ShippingAddress", field: "shippingAddress", sortable: true, isShowable: true, },
      { headerName: "SubtotalPrice", field: "subtotalPrice", sortable: true, isShowable: true, },
      { headerName: "TaxesIncluded", field: "taxesIncluded", sortable: true, isShowable: true, },
      { headerName: "TotalDiscounts", field: "totalDiscounts", sortable: true, isShowable: true, },
      { headerName: "TotalLineItemsPrice", field: "totalLineItemsPrice", sortable: true, isShowable: true, },
      { headerName: "TotalTipReceived", field: "totalTipReceived", sortable: true, isShowable: true, },
      { headerName: "TotalPrice", field: "totalPrice", sortable: true, isShowable: true, },
      { headerName: "TotalTax", field: "totalTax", sortable: true, isShowable: true, },
      { headerName: "TotalWeight", field: "totalWeight", sortable: true, isShowable: true, },
      { headerName: "UserId", field: "userId", sortable: true, isShowable: true, },
      { headerName: "PresentmentCurrency", field: "presentmentCurrency", sortable: true, isShowable: true, },
      { headerName: "TotalOutstanding", field: "totalOutstanding", sortable: true, isShowable: true, },
      { headerName: "EstimatedTaxes", field: "estimatedTaxes", sortable: true, isShowable: true, },
      { headerName: "CurrentSubtotalPrice", field: "currentSubtotalPrice", sortable: true, isShowable: true, },
      { headerName: "CurrentTotalDiscounts", field: "currentTotalDiscounts", sortable: true, isShowable: true, },
      { headerName: "CurrentTotalPrice", field: "currentTotalPrice", sortable: true, isShowable: true, },
      { headerName: "CurrentTotalTax", field: "currentTotalTax", sortable: true, isShowable: true, },
      { headerName: "TaxExempt", field: "taxExempt", sortable: true, isShowable: true, },
      { headerName: "CancelReason", field: "cancelReason", sortable: true, isShowable: true, },
      {
        headerName: "CancelledAt", field: "cancelledAt", sortable: true, isShowable: true,
        valueFormatter: (params) => {
          if (params && params.value) {
            let value = params.value;
            let date = moment(value, "DD-MM-YYYY hh:mm:ss");
            if (date.isValid()) {
              value = date.format("DD-MM-YYYY hh:mm:ss");
            }
            return value;
          }
          return '';
        }
      },
    ];
  }

  syncOrders() {
    this.saleService.syncOrders().subscribe((result) => {
    });
  }

  getData(params): Observable<{ data; totalCount }> {
    console.log(params);
    this.orderParams.pageNumber = params.startRow;
    this.orderParams.pageSize = params.endRow;
    return this.saleService.getSales(this.orderParams)
  }

  onButtonClick(params) {
    console.log(params);
    if (params.button.key == 'View') {
      this.router.navigateByUrl(`admin/sales/order-detail/${params.data.internalFulFillmentOrderId}`)
    }
    if (params.button.key == 'Accept') {
      this.acceptOrder(params.data);
    }
  }

  acceptOrder(order) {
    var model = {
      id: order.id,
      ShopifyId: order.ShopifyId,
      FulfillmentOrderId: order.fulFillmentOrderId,
    };
    this.orderService.acceptOrder(model).subscribe(res => {
      if (res.succeeded) {
        this.toastr.success(res.messages[0])
      }
      else {
        this.toastr.error(res.messages[0])
      }
    })
  }
}

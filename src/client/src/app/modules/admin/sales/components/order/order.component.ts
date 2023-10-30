import { OrderDetailComponent } from './order-detail/order-detail.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Order } from '../../models/order';
import { OrderParams } from '../../models/orderParams';
import { ToastrService } from 'ngx-toastr';
import { Sort } from '@angular/material/sort';
import { TableColumn } from 'src/app/core/shared/components/table/table-column';
import { SalesService } from '../../services/sales.service';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { OrderStatusMapping } from 'src/app/core/enums/OrderStatus';
import { CustomAction } from 'src/app/core/shared/components/table/custom-action';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {
  // orderColumns: TableColumn[];
  orderParams = new OrderParams();
  searchString: string;

  displayedColumns: string[] = ['id', 'referenceNumber', 'timeStamp', 'customerName', 'total', 'isPaid', 'action'];
  dataSource: PaginatedResult<Order>;

  constructor(
    public saleService: SalesService,
    public dialog: MatDialog,
    public toastr: ToastrService,
    public router: Router
  ) { }

  ngOnInit(): void {
    this.getOrders();
    this.initOvertimeColumns();
  }

  getOrders(): void {
    this.saleService.getSales(this.orderParams).subscribe((result) => {
      this.dataSource = result;
      this.orderData = result.data;
    });

  }

  pageChanged(event: PaginatedFilter): void {
    this.orderParams.pageNumber = event.pageNumber;
    this.orderParams.pageSize = event.pageSize;
    this.getOrders();
  }

  sort($event: Sort): void {
    this.orderParams.orderBy = $event.active + ' ' + $event.direction;
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
    this.orderParams.searchString = '';
    this.orderParams.pageNumber = 0;
    this.orderParams.pageSize = 0;
    this.getOrders();
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

  openEditPOS(orderId: string) {
    console.log(orderId);
  }


  public OrderStatusMapping = OrderStatusMapping;
  orderData: any[] = [];
  orderColumns: any[];
  private AgGrid: AgGridBaseComponent;
  @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
    if (content) {
      // initially setter gets called with undefined
      this.AgGrid = content;
    }
  }
  gridReady(event): void {
    if (this.AgGrid) {
      // this.AgGrid.gridApi.setDatasource(this.scrollBarDataSource);
    }
    // this.getOvertimeMyQueue();
    this.getOrders();
  }
  onSaveButtonClick(params) {
    console.log(params);
    this.router.navigateByUrl(`admin/sales/order-detail/${params.data.id}`)
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
          onClick: this.onSaveButtonClick.bind(this)
        },
        width: 60,
        pinned: "left"
      },
      { headerName: "Id", field: "shopifyId", sortable: true, isShowable: true, width: 100 },
      {
        headerName: "Customer Name", field: "customerName", sortable: true, isShowable: true, width: 256,
        valueGetter: 'data.shippingAddress.name'
      },
      { headerName: "Customer Phone", field: "phone", sortable: true, isShowable: true, width: 256 },
      { headerName: "Customer Email", field: "email", sortable: true, isShowable: true, width: 120 },
      {
        headerName: "Status", field: "status", sortable: true, isShowable: true,
        valueGetter: params => {
          return OrderStatusMapping[params.data.status];;
        }
      },
      { headerName: "Note", field: "note", sortable: true, isShowable: true, },
      { headerName: "Confirmed", field: "confirmed", sortable: true, isShowable: true, },
      { headerName: "CreatedAt", field: "createdAt", sortable: true, isShowable: true, },
      { headerName: "Currency", field: "currency", sortable: true, isShowable: true, },
      { headerName: "CustomerLocale", field: "customerLocale", sortable: true, isShowable: true, },
      { headerName: "Email", field: "email", sortable: true, isShowable: true, },
      { headerName: "FinancialStatus", field: "financialStatus", sortable: true, isShowable: true, },
      { headerName: "FulfillmentStatus", field: "fulfillmentStatus", sortable: true, isShowable: true, },
      { headerName: "Phone", field: "phone", sortable: true, isShowable: true, },
      { headerName: "Tags", field: "tags", sortable: true, isShowable: true, },
      { headerName: "LandingSite", field: "landingSite", sortable: true, isShowable: true, },
      { headerName: "LocationId", field: "locationId", sortable: true, isShowable: true, },
      { headerName: "Name", field: "name", sortable: true, isShowable: true, },
      { headerName: "OrderNumber", field: "orderNumber", sortable: true, isShowable: true, },
      { headerName: "OrderStatusUrl", field: "orderStatusUrl", sortable: true, isShowable: true, },
      {
        headerName: "PaymentGatewayNames", field: "paymentGatewayNames", sortable: true, isShowable: true,
      },
      { headerName: "ProcessedAt", field: "processedAt", sortable: true, isShowable: true, },
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
      { headerName: "CancelledAt", field: "cancelledAt", sortable: true, isShowable: true, },
    ];
  }
  syncOrders() {
    this.saleService.syncOrders().subscribe((result) => {
    });
  }
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SalesComponent } from './sales.component';
import { SalesRoutingModule } from './sales-routing.module';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { OrderComponent } from './components/order/order.component';
import { OrderDetailComponent } from './components/order/order-detail/order-detail.component';
import { PurchaseOrderComponent } from './components/purchase-order/purchase-order.component';
import { POListComponent } from './components/purchase-order/polist/polist.component';
import { StockInComponent } from './components/stock/stock-in/stock-in.component';
import { StockInListComponent } from './components/stock/stock-in-list/stock-in-list.component';
import { StockInDetailComponent } from './components/stock/stock-in-detail/stock-in-detail.component';
import { ApprovalListComponent } from './components/stock/approval-list/approval-list.component';
import { PoEditComponent } from './components/purchase-order/po-edit/po-edit.component';
import { PoDetailComponent } from './components/purchase-order/po-detail/po-detail.component';
import { StockInEditComponent } from './components/stock/stock-in-edit/stock-in-edit.component';
import { StockOutComponent } from './components/stockout/stock-out/stock-out.component';
import { StockOutListComponent } from './components/stockout/stock-out-list/stock-out-list.component';
import { StockOutDetailComponent } from './components/stockout/stock-out-detail/stock-out-detail.component';
import { StockOutEditComponent } from './components/stockout/stock-out-edit/stock-out-edit.component';
import { VarianceReportComponent } from './reports/variance-report/variance-report.component';
import { UpdatePromotionComponent } from './reports/variance-report/update-promotion/update-promotion.component';
import { StockReportComponent } from './reports/stock-report/stock-report.component';
import { StockOutReportComponent } from './reports/stock-out-report/stock-out-report.component';

@NgModule({
  declarations: [
    SalesComponent,
    OrderComponent,
    OrderDetailComponent,
    PurchaseOrderComponent,
    POListComponent,
    StockInComponent,
    StockInListComponent,
    StockInDetailComponent,
    ApprovalListComponent,
    PoEditComponent,
    PoDetailComponent,
    StockInEditComponent,
    StockOutComponent,
    StockOutListComponent,
    StockOutDetailComponent,
    StockOutEditComponent,
    VarianceReportComponent,
    UpdatePromotionComponent,
    StockReportComponent,
    StockOutReportComponent
  ],
  imports: [
    CommonModule,
    SalesRoutingModule,
    MaterialModule,
    SharedModule
  ]
})
export class SalesModule { }

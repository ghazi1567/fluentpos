import { OrderComponent } from './components/order/order.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchaseOrderComponent } from './components/purchase-order/purchase-order.component';
import { POListComponent } from './components/purchase-order/polist/polist.component';
import { StockInComponent } from './components/stock/stock-in/stock-in.component';
import { StockInListComponent } from './components/stock/stock-in-list/stock-in-list.component';
import { ApprovalListComponent } from './components/stock/approval-list/approval-list.component';
import { PoEditComponent } from './components/purchase-order/po-edit/po-edit.component';
import { StockInEditComponent } from './components/stock/stock-in-edit/stock-in-edit.component';
import { StockOutComponent } from './components/stockout/stock-out/stock-out.component';
import { StockOutListComponent } from './components/stockout/stock-out-list/stock-out-list.component';
import { StockOutEditComponent } from './components/stockout/stock-out-edit/stock-out-edit.component';
import { VarianceReportComponent } from './reports/variance-report/variance-report.component';
import { StockReportComponent } from './reports/stock-report/stock-report.component';

const routes: Routes = [
  {
    path: 'orders',
    component: OrderComponent
  },
  {
    path: 'purchase-order',
    component: PurchaseOrderComponent
  },
  {
    path: 'po-edit/:id',
    component: PoEditComponent
  },
  {
    path: 'purchase-order-list',
    component: POListComponent
  },
  {
    path: 'stock-in',
    component: StockInComponent
  },
  {
    path: 'stock-in-list',
    component: StockInListComponent
  },
  {
    path: 'approval-list',
    component: ApprovalListComponent
  },
  {
    path: 'stock-in-edit/:id',
    component: StockInEditComponent
  },
  {
    path: 'stock-out',
    component: StockOutComponent
  },
  {
    path: 'stock-out-list',
    component: StockOutListComponent
  },
  {
    path: 'stock-out-edit/:id',
    component: StockOutEditComponent
  },
  {
    path: 'variance-report',
    component: VarianceReportComponent
  },
  {
    path: 'stock-report',
    component: StockReportComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule { }

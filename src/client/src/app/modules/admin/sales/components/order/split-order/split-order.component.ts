import { Component, Inject, OnInit } from '@angular/core';
import { WarehouseService } from '../../../services/warehouse.service';
import { OrdersService } from '../../../services/orders.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-split-order',
  templateUrl: './split-order.component.html',
  styleUrls: ['./split-order.component.scss']
})
export class SplitOrderComponent implements OnInit {
  order: any;
  warehouseData: any[];
  selectedObject = {
    locationId: null
  }
  constructor(public dialogRef: MatDialogRef<SplitOrderComponent>,
    @Inject(MAT_DIALOG_DATA) public _data: any,
    private warehouseService: WarehouseService,
    private toastr: ToastrService,
    private orderService: OrdersService) {
    this.order = _data;
  }

  ngOnInit(): void {
    this.getWarehouses();
  }

  getWarehouses() {
    this.warehouseService.getAll().subscribe(res => {
      this.warehouseData = res.data;
    })
  }

  changeOrderLocation() {
    var model = {
      id: this.order.id,
      shopifyId: this.order.shopifyId,
      newLocationId: this.selectedObject.locationId,
    }
    this.orderService.moveLocation(model).subscribe((res) => {
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
  onNoClick(): void {
    this.dialogRef.close(this.order);
  }
}

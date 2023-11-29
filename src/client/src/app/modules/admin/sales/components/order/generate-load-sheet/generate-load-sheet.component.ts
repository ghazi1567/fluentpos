import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { OrdersService } from '../../../services/orders.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-generate-load-sheet',
  templateUrl: './generate-load-sheet.component.html',
  styleUrls: ['./generate-load-sheet.component.scss']
})
export class GenerateLoadSheetComponent implements OnInit {
  searchText: any
  loadSheetOrder: any[] = []
  loadSheetMain: any = {
    TotalOrder: 0,
    TotalAmount: 0,
    Details: []
  }
  firstForm: FormGroup;
  sr: number = 1
  selectedItem = '2';
  warehouseData: any[];
  isWarehouseSelected = false;
  constructor(private orderService: OrdersService,
    private fb: FormBuilder,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getLookup();

    this.firstForm = this.fb.group({
      searchText: ['', Validators.required],
      warehouseId: ['', Validators.required],
    });
  }
  getLookup() {
    this.orderService.getWarehouseLookup().subscribe(res => {
      this.warehouseData = res.data;
    })
  }
  warehouseChange($event) {
    console.log($event);
    this.isWarehouseSelected = true;
  }
  resetWarehouse() {
    this.firstForm.controls['warehouseId'].setValue('');
    this.isWarehouseSelected = false;
    this.loadSheetOrder = []
  }
  getNextRank() {
    this.sr++;
    return this.sr;
  }
  scanLoadSheetOrder() {
    var model = this.firstForm.value;
    if (!model.warehouseId) {
      this.toastr.error('Please select outlet.');
    }

    this.orderService.scanLoadSheetOrder(model).subscribe((res) => {
      console.log(res);
      var exist = this.loadSheetOrder.find(x => x.fulfillmentOrderId == res.data.shopifyId)
      if (exist) {
        this.toastr.error('Order already exist');
      } else {
        this.loadSheetOrder.push({
          sr: this.sr++,
          internalOrderId: res.data.internalOrderId,
          city: res.data.fulfillmentOrderDestination.city,
          totalQuantity: res.data.totalQuantity,
          name: res.data.name,
          fulfillmentOrderId: res.data.shopifyId,
          warehouseId: res.data.warehouseId,
          trackingNumber: res.data.trackingNumber,
          totalPrice: res.data.totalPrice,
        });
      }
    });
  }

  generateLoadSheet() {
    if (this.loadSheetOrder.length == 0) {
      this.toastr.error('Please scan order for loadsheet');
      return;
    }
    var model = this.firstForm.value;
    var warehouse = this.warehouseData.find(x => x.id == model.warehouseId);

    const sumTotalAmount = this.loadSheetOrder.reduce((accumulator, object) => {
      return accumulator + object.totalPrice;
    }, 0);

    this.loadSheetMain = {
      TotalOrder: this.loadSheetOrder.length,
      TotalAmount: sumTotalAmount,
      Details: this.loadSheetOrder,
      contactNumber: warehouse.phone,
      pickupAddress: warehouse.address1,
      cityName: warehouse.city,
    }


    this.orderService.generateLoadSheet(this.loadSheetMain).subscribe(res => {
      if (res.succeeded) {
        this.toastr.success(res.messages[0]);
        this.loadSheetMain = {
          TotalOrder: 0,
          TotalAmount: 0,
          Details: []
        }
      } else {
        this.toastr.error(res.messages[0]);
      }
    })
  }
}

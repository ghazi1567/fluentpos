import { Component, OnInit, ViewChild } from '@angular/core';
import { OrdersService } from '../../../services/orders.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NbStepperComponent } from '@nebular/theme';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-confirm-order',
  templateUrl: './confirm-order.component.html',
  styleUrls: ['./confirm-order.component.scss']
})
export class ConfirmOrderComponent implements OnInit {
  @ViewChild("stepper") stepper: NbStepperComponent;
  order: any;
  linearMode = true;
  orderNo: any;
  stepperIndex: number = 0; // here 0 is initial index
  confirmedItems: any[] = [];
  isSppiner = false;

  firstForm: FormGroup;
  secondForm: FormGroup;
  thirdForm: FormGroup;

  constructor(private orderService: OrdersService,
    private fb: FormBuilder,
    private toastr: ToastrService) {
  }

  ngOnInit() {
    this.firstForm = this.fb.group({
      orderNo: ['', Validators.required],
    });

    this.secondForm = this.fb.group({
      barcode: ['', Validators.required],
    });

    this.thirdForm = this.fb.group({
      thirdCtrl: ['', Validators.required],
    });
  }

  getOrder(orderNo) {
    var _orderNo = orderNo.replace(/^#/g, "");
    this.orderService.getOrderForConfirm(_orderNo).subscribe((response) => {
      if (response.succeeded) {
        this.order = response.data;
        this.order.lineItems.forEach(item => {
          item.confirmedQty = 0;
        });
        this.stepper.next();
      } else {
        this.toastr.error(response.messages[0])
      }
    });
  }

  onFirstSubmit() {
    console.log(this.firstForm.value)
    var model = this.firstForm.value;
    this.getOrder(model.orderNo);
    this.firstForm.markAsDirty();
    this.stepperIndex = 1;
  }

  onSecondSubmit() {
    var model = this.secondForm.value;
    var item = this.order.lineItems.find(x => x.sku == model.barcode)
    if (item) {
      if (item.confirmedQty == item.quantity) {
        this.toastr.error(`Required qty (${item.quantity}) already confirmed for this order.`);
        return;
      } else {
        this.order.lineItems.forEach(item => {
          if (item.sku == model.barcode) {
            item.confirmedQty = item.confirmedQty + 1
            this.toastr.success(`SKU# ${model.barcode} confirmed.`);
          }
        });
      }

    } else {
      this.toastr.error(`SKU# ${model.barcode} not found in this order.`);
    }

    this.secondForm.markAsDirty();
    var isCompleted = true;
    this.confirmedItems = [];
    this.order.lineItems.forEach(item => {
      if (item.confirmedQty != item.quantity) {
        isCompleted = false;
      } else if (item.confirmedQty == item.quantity) {
        this.confirmedItems.push(item);
      }
    });

    if (isCompleted == true) {
      this.stepper.next();
      this.confirmOrder();
    }
  }

  confirmOrder() {
    var model = {
      id: this.order.id,
      ShopifyId: this.order.ShopifyId,
      LineItems: this.confirmedItems,
      FulfillmentOrderId: this.order.fulfillmentOrderId
    };
    this.isSppiner = true;
    this.orderService.confirmOrder(model).subscribe((response) => {
      this.isSppiner = false;
      this.stepper.next();
      window.location.reload();
    });
  }
}

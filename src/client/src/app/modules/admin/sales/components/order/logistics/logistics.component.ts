import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { OrdersService } from "../../../services/orders.service";
import { Router } from "@angular/router";

@Component({
    selector: "app-logistics",
    templateUrl: "./logistics.component.html",
    styleUrls: ["./logistics.component.scss"]
})
export class LogisticsComponent implements OnInit {
    searchText: any;
    scannedOrders: any[] = [];
    loadSheetMain: any = {
        TotalOrder: 0,
        TotalAmount: 0,
        Details: []
    };
    firstForm: FormGroup;
    sr: number = 1;
    selectedItem = "2";
    warehouseData: any[];
    isWarehouseSelected = false;
    constructor(private orderService: OrdersService, private fb: FormBuilder, private toastr: ToastrService, public router: Router) {}

    ngOnInit(): void {
        this.getLookup();

        this.firstForm = this.fb.group({
            trackingNumber: ["", Validators.required]
        });
    }
    getLookup() {
        this.orderService.getWarehouseLookup().subscribe((res) => {
            this.warehouseData = res.data;
        });
    }
    warehouseChange($event) {
        console.log($event);
        this.isWarehouseSelected = true;
    }
    resetWarehouse() {
        this.firstForm.controls["warehouseId"].setValue("");
        this.isWarehouseSelected = false;
        this.scannedOrders = [];
    }
    getNextRank() {
        this.sr++;
        return this.sr;
    }
    scanTrackingNumber() {
        var model = this.firstForm.value;
        this.orderService.getByTrackingNumber(model.trackingNumber).subscribe((res) => {
            console.log(res);
            this.scannedOrders.push(res.data);
        });
    }

    removeOrder(index) {
        this.scannedOrders.splice(index, 1);
    }
    viewOrder(data) {
        // Converts the route into a string that can be used
        // with the window.open() function
        const url = this.router.serializeUrl(this.router.createUrlTree([`admin/sales/order-detail/${data.internalFulFillmentOrderId}`]));

        window.open(url, "_blank");
    }

    UpdateOrder() {
        var orders = [];
        this.scannedOrders.forEach((x) => {
            orders.push({
                OrderId: x.id,
                FulfillmentOrderId: x.internalFulFillmentOrderId,
                TrackingNumber: x.trackingNumber
            });
        });
        var model = {
            Orders: orders
        };

        this.orderService.deliverdOrder(model).subscribe((res) => {
            this.toastr.success(res.messages[0]);
        });
    }
}

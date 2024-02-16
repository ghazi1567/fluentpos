import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { OrdersService } from "../../../services/orders.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "app-generate-load-sheet",
    templateUrl: "./generate-load-sheet.component.html",
    styleUrls: ["./generate-load-sheet.component.scss"]
})
export class GenerateLoadSheetComponent implements OnInit {
    searchText: any;
    loadSheetOrder: any[] = [];
    loadSheetMain: any = {
        id: 0,
        totalOrder: 0,
        totalAmount: 0,
        details: [],
        contactNumber: "",
        pickupAddress: "",
        cityName: "",
        warehouseId: 0
    };
    firstForm: FormGroup;
    sr: number = 1;
    selectedItem = "2";
    warehouseData: any[];
    isWarehouseSelected = false;
    loadsheetId = 0;
    constructor(private orderService: OrdersService, private fb: FormBuilder, private toastr: ToastrService, private route: ActivatedRoute) {
        var loadsheetId = this.route.snapshot.params["id"];
        if (loadsheetId) {
            this.loadsheetId = loadsheetId;
        }
        console.log(this.loadsheetId);
    }

    ngOnInit(): void {
        this.firstForm = this.fb.group({
            searchText: ["", Validators.required],
            warehouseId: ["", Validators.required]
        });
        this.getLookup();
        if (this.loadsheetId > 0) {
            this.getLoasheet();
        }
    }
    getLoasheet() {
        this.orderService.getLoadsheetById(this.loadsheetId).subscribe((res) => {
            this.loadSheetMain = res.data;
            this.loadSheetOrder = this.loadSheetMain.details;
            this.firstForm.controls["warehouseId"].setValue(this.loadSheetMain.warehouseId);
            this.isWarehouseSelected = true;
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
        this.loadSheetOrder = [];
    }
    getNextRank() {
        this.sr++;
        return this.sr;
    }
    scanLoadSheetOrder() {
        var model = this.firstForm.value;
        if (!model.warehouseId) {
            this.toastr.error("Please select outlet.");
        }

        this.orderService.scanLoadSheetOrder(model).subscribe((res) => {
            console.log(res);
            var exist = this.loadSheetOrder.find((x) => x.fulfillmentOrderId == res.data.fulFillmentOrderId);
            if (exist) {
                this.toastr.error("Order already exist");
            } else {
                this.loadSheetOrder.push({
                    sr: this.sr++,
                    city: res.data.city,
                    totalQuantity: res.data.lineitemCount,
                    orderNumber: res.data.orderNumber,
                    fulFillmentOrderId: res.data.fulFillmentOrderId,
                    internalFulFillmentOrderId: res.data.internalFulFillmentOrderId,
                    internalOrderId: res.data.internalOrderId,
                    orderId: res.data.orderId,
                    warehouseId: res.data.warehouseId,
                    trackingNumber: res.data.trackingNumber,
                    totalAmount: res.data.totalPrice
                });
            }
        });
    }

    generateLoadSheet() {
        if (this.loadSheetOrder.length == 0) {
            this.toastr.error("Please scan order for loadsheet");
            return;
        }
        var model = this.firstForm.value;
        var warehouse = this.warehouseData.find((x) => x.id == model.warehouseId);

        const sumTotalAmount = this.loadSheetOrder.reduce((accumulator, object) => {
            return accumulator + object.totalAmount;
        }, 0);

        this.loadSheetMain.totalOrder = this.loadSheetOrder.length;
        this.loadSheetMain.totalAmount = sumTotalAmount;
        this.loadSheetMain.details = this.loadSheetOrder;
        this.loadSheetMain.contactNumber = warehouse.phone;
        this.loadSheetMain.pickupAddress = warehouse.pickupAddress;
        this.loadSheetMain.cityName = warehouse.city;
        this.loadSheetMain.warehouseId = warehouse.id;

        this.orderService.generateLoadSheet(this.loadSheetMain).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.loadSheetMain = {
                    TotalOrder: 0,
                    TotalAmount: 0,
                    Details: []
                };
            } else {
                this.toastr.error(res.messages[0]);
            }
        });
    }

    removeOrder(item: any) {
        item.isDeleted = true;
        // const data = this.loadSheetOrder;
        // let index: number = data.findIndex((d) => d === item);
        // data.splice(index, 1);
        // this.loadSheetOrder = data;
    }
}

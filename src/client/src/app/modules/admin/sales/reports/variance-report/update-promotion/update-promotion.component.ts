import { Component, Inject, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { VaraintReport } from "src/app/modules/admin/catalog/models/variantReport";
import { ReportService } from "../../../services/report.service";

@Component({
    selector: "app-update-promotion",
    templateUrl: "./update-promotion.component.html",
    styleUrls: ["./update-promotion.component.scss"]
})
export class UpdatePromotionComponent implements OnInit {
    timeStamp: any = new Date();
    isLoader = false;

    constructor(@Inject(MAT_DIALOG_DATA) public data: VaraintReport[],
    private reportService:ReportService) {
        console.log(this.data);
    }

    ngOnInit(): void {}

    onSave() {
        let products = [];
        if (this.data) {
            this.data.forEach((x) => {
                products.push({
                    ProductId: x.productId,
                    Barcode: x.barcodeSymbology,
                    FactorDate: this.timeStamp,
                    FactorAmount: x.promotionMode
                });
            });
        }
        var model = {
            Products: products,
            updateFrom: this.timeStamp
        };
        this.reportService.updatePromotion(model).subscribe(res=>{
          console.log(res);
        })
    }
}

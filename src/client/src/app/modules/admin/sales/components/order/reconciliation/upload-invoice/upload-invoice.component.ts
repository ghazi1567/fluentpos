import { Component, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Upload } from "src/app/core/models/uploads/upload";
import { UploadType } from "src/app/core/models/uploads/upload-type";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { environment } from "src/environments/environment";
import { InventoryLevelService } from "../../../../services/inventory-level.service";
import { InvoicesService } from "../../../../services/invoices.service";

@Component({
    selector: "app-upload-invoice",
    templateUrl: "./upload-invoice.component.html",
    styleUrls: ["./upload-invoice.component.scss"]
})
export class UploadInvoiceComponent implements OnInit {
    upload = new Upload();
    csvMapping: CsvMapping[] = [
        { gridColumn: "CPRNumber", csvColumn: "CPRNumber" },
        { gridColumn: "CPRDate", csvColumn: "CPRDate" },
        { gridColumn: "OrderNo", csvColumn: "OrderNo" },
        { gridColumn: "TrackingNumber", csvColumn: "TrackingNumber" },
        { gridColumn: "Weight", csvColumn: "Weight" },
        { gridColumn: "PickupDate", csvColumn: "PickupDate" },
        { gridColumn: "OriginCity", csvColumn: "OriginCity" },
        { gridColumn: "DeliveryCity", csvColumn: "DeliveryCity" },
        { gridColumn: "Status", csvColumn: "Status" },
        { gridColumn: "CODAmount", csvColumn: "CODAmount" },
        { gridColumn: "UpfrontAmount", csvColumn: "UpfrontAmount" },
        { gridColumn: "ReserveAmount", csvColumn: "ReserveAmount" },
        { gridColumn: "DeliverDate", csvColumn: "DeliverDate" },
        { gridColumn: "ShippingCharges", csvColumn: "ShippingCharges" },
        { gridColumn: "UpfrontCharges", csvColumn: "UpfrontCharges" },
        { gridColumn: "NetAmount", csvColumn: "NetAmount" },
        { gridColumn: "Tax", csvColumn: "Tax" },
        { gridColumn: "NetAmountReceivable", csvColumn: "NetAmountReceivable" }
    ];
    inventoryColumns: any[] = [];

    inventoryData: any[] = [];
    warehouseData: any[] = [];
    rowClassRules: any;
    constructor(private csvParser: CsvParserService, private invoicesService: InvoicesService, private toastr: ToastrService, private router: Router) {}

    ngOnInit(): void {
        this.initInventoryColumns();
    }

    onDownloadSampleFile() {
        var url = environment.apiFileUrl + "/sample/sample-invoice.xlsx";
        var win = window.open(url, "_blank");
    }
    gridOptions = [];
    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }

    gridReady(event): void {}

    initInventoryColumns(): void {
        this.inventoryColumns = [
            { headerName: "Order Ref.", field: "OrderNo", floatingFilter: false, filter: false, width: 160 },
            { headerName: "TrackingNumber", field: "TrackingNumber", floatingFilter: false, filter: false, isShowable: true, width: 160 },
            { headerName: "Weight(kg)", field: "Weight", floatingFilter: false, filter: false, isShowable: true, width: 120 },
            { headerName: "PickupDate", field: "PickupDate", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "OriginCity", field: "OriginCity", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "DeliveryCity", field: "DeliveryCity", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "Status", field: "Status", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "CODAmount", field: "CODAmount", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "UpfrontAmount", field: "UpfrontAmount", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "ReserveAmount", field: "ReserveAmount", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "D/RDate", field: "DeliverDate", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "ShippingCharges", field: "ShippingCharges", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "UpfrontCharges", field: "UpfrontCharges", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "NetAmount", field: "NetAmount", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "Tax", field: "Tax", floatingFilter: false, filter: false, isShowable: true, width: 150 },
            { headerName: "Net Amount receivable", floatingFilter: false, field: "NetAmountReceivable", filter: false, isShowable: true, width: 150 }
        ];
    }

    handleFileSelect(evt) {
        var files = evt.target.files; // FileList object
        // Parse the file you want to select for the operation along with the configuration
        this.csvParser
            .parseXlsx(files[0], {
                header: true,
                delimiter: ",",
                mapping: this.csvMapping
            })
            .pipe()
            .subscribe(
                (result: Array<any>) => {
                    console.log("Result", result);
                },
                (error: NgxCSVParserError) => {
                    console.log("Error", error);
                }
            );
    }
    removeDuplicates(array, properties) {
        let seen = {};
        return array.filter((obj) => {
            let key = properties.map((prop) => obj[prop]).join("|");
            if (seen[key]) {
                return false;
            } else {
                seen[key] = true;
                return true;
            }
        });
    }
    onSelectFile(event) {
        if (event.target.files && event.target.files[0]) {
            var reader = new FileReader();
            reader.readAsDataURL(event.target.files[0]); // read file as data url

            this.upload.fileName = event.target.files[0].name.split(".").shift();
            this.upload.extension = event.target.files[0].name.split(".").pop();
            this.upload.uploadType = UploadType.Inventory;

            reader.onloadend = (event) => {
                // called once readAsDataURL is completed
                this.upload.data = event.target.result;
            };

            this.csvParser
                .parseXlsx(event.target.files[0], {
                    header: true,
                    delimiter: ",",
                    mapping: this.csvMapping
                })
                .pipe()
                .subscribe(
                    (result: Array<any>) => {
                        console.log("Result", result);
                        this.inventoryData = result;
                    },
                    (error: NgxCSVParserError) => {
                        console.log("Error", error);
                    }
                );
        }
    }

    saveInventory() {
        console.log(this.inventoryData);
        if (this.inventoryData.length == 0) {
            this.toastr.error("Please upload invoice data");
            return;
        }

        var model = {
            CPRNumber: this.inventoryData[0].CPRNumber,
            CPRDate: this.inventoryData[0].CPRDate,
            InvoiceDetails: this.inventoryData
        };
        this.invoicesService.createInvoice(model).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.router.navigateByUrl("/admin/sales/reconciliation");
            } else {
                this.toastr.error(res.messages[0]);
            }
        });
    }
}

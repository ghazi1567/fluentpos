import { Component, OnInit, ViewChild } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { OrdersService } from "../../../services/orders.service";
import { InvoicesService } from "../../../services/invoices.service";

@Component({
    selector: "app-reconciliation",
    templateUrl: "./reconciliation.component.html",
    styleUrls: ["./reconciliation.component.scss"]
})
export class ReconciliationComponent implements OnInit {
    invoiceColumns: any[] = [];
    searchParams = new SearchParams();
    invoiceData: any[] = [];
    constructor(private invoicesService: InvoicesService, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.initInventoryColumns();
    }

    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }

    gridReady(event): void {
        this.getInvoiceData();
    }
    onButtonClick(params) {
        console.log(params);
        if (params.button.key == "Print") {
            // this.router.navigateByUrl(`admin/sales/order-detail/${params.data.internalFulFillmentOrderId}`)
        }
        if (params.button.key == "Regenerate") {
        }
    }

    actionButtons: CustomAction[] = [new CustomAction("Print", "Print", "View", "print"), new CustomAction("Regenerate", "Regenerate", "Update", "refresh")];
    initInventoryColumns(): void {
        this.invoiceColumns = [
            {
                headerName: "",
                cellRenderer: "buttonRenderer",
                filter: false,
                sortable: false,
                cellRendererParams: {
                    buttons: ["View"],
                    actionButtons: this.actionButtons,
                    onClick: this.onButtonClick.bind(this)
                },
                pinned: "left",
                width: 60
            },
            { headerName: "CPR Number", field: "cprNumber", sortable: true, isShowable: true, width: 235 },
            { headerName: "CPR Date", field: "cprDate", sortable: true, isShowable: true, width: 235 },
            { headerName: "ToTal COD Amount", field: "toTalCODAmount", sortable: true, isShowable: true, width: 235 },
            { headerName: "Total Reserve Amount", field: "totalReserveAmount", sortable: true, isShowable: true, width: 235 },
            { headerName: "Total Shipping Charges", field: "totalShippingCharges", sortable: true, isShowable: true },
            { headerName: "Total Net Amount", field: "totalNetAmount", sortable: true, isShowable: true, width: 256 },
            { headerName: "Total Tax", field: "totalTax", sortable: true, isShowable: true, width: 256 },
            { headerName: "Total Receivable", field: "totalReceivable", sortable: true, isShowable: true, width: 256 },
            { headerName: "Total Delivered", field: "totalDelivered", sortable: true, isShowable: true, width: 256 },
            { headerName: "Total Returned", field: "totalReturned", sortable: true, isShowable: true, width: 256 }
        ];
    }

    getInvoiceData() {
        this.searchParams.pageNumber = 0;
        this.searchParams.pageSize = 100;
        this.invoicesService.getAlls(this.searchParams).subscribe((res) => {
            this.invoiceData = res.data;
        });
    }
}

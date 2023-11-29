import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from "moment";
import { OrdersService } from '../../../services/orders.service';
import { SearchParams } from 'src/app/core/models/Filters/SearchParams';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { CustomAction } from 'src/app/core/shared/components/table/custom-action';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-load-sheets',
  templateUrl: './load-sheets.component.html',
  styleUrls: ['./load-sheets.component.scss']
})
export class LoadSheetsComponent implements OnInit {
  loadSheetColumns: any[] = [];
  searchParams = new SearchParams();
  loadSheetData: any[] = [];
  constructor(private orderService: OrdersService,
    private toastr: ToastrService) { }

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
    this.getLoadsheets();
  }
  onButtonClick(params) {
    console.log(params);
    if (params.button.key == 'Print') {
      // this.router.navigateByUrl(`admin/sales/order-detail/${params.data.internalFulFillmentOrderId}`)
    }
    if (params.button.key == 'Regenerate') {
      this.reGenerateLoadsheet(params.data);
    }
  }

  actionButtons: CustomAction[] = [
    new CustomAction("Print", "Print", "View", "print"),
    new CustomAction("Regenerate", "Regenerate", "Update", "refresh"),
  ];
  initInventoryColumns(): void {
    this.loadSheetColumns = [
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
        width: 120
      },
      { headerName: "Contact Number", field: "contactNumber", sortable: true, isShowable: true, width: 235 },
      { headerName: "Pickup Address", field: "pickupAddress", sortable: true, isShowable: true, width: 235 },
      { headerName: "City Name", field: "cityName", sortable: true, isShowable: true, width: 235 },
      { headerName: "Total Order", field: "totalOrder", sortable: true, isShowable: true, width: 235 },
      { headerName: "Total Amount", field: "totalAmount", sortable: true, isShowable: true },
      { headerName: "Status", field: "status", sortable: true, isShowable: true, width: 256 },
      {
        headerName: "Note", field: "note", sortable: true, width: 350,
        wrapText: true,
        autoHeight: true,
      },
    ];
  }



  getLoadsheets() {
    this.searchParams.pageNumber = 0
    this.searchParams.pageSize = 100;
    this.orderService.getLoadsheets().subscribe(res => {
      this.loadSheetData = res.data;
    })
  }

  reGenerateLoadsheet(loadSheet) {
    var model = {
      id: loadSheet.id,
    };
    this.orderService.reGenerateLoadSheet(model).subscribe(res => {
      if (res.succeeded) {
        this.toastr.success(res.messages[0])
      }
      else {
        this.toastr.error(res.messages[0])
      }
    })
  }
}

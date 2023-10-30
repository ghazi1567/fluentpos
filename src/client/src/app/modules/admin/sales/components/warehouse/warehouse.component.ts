import { Component, OnInit, ViewChild } from '@angular/core';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { WarehouseService } from '../../services/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss']
})
export class WarehouseComponent implements OnInit {
  warehousesData: any[] = [];
  warehouseColumns: any[] = [];
  constructor(private warehouseService: WarehouseService) { }

  ngOnInit(): void {
    this.initWarehouseColumns();
  }

  getWarehouse() {
    this.warehouseService.getAll().subscribe(res => {
      this.warehousesData = res.data;
    })
  }

  private AgGrid: AgGridBaseComponent;
  @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
    if (content) {
      // initially setter gets called with undefined
      this.AgGrid = content;
    }
  }

  gridReady(event): void {
    this.getWarehouse();
  }
  initWarehouseColumns(): void {
    this.warehouseColumns = [
      { headerName: "Shopify Id", field: "shopifyId", sortable: true, isShowable: true, width: 256 },
      { headerName: "Name", field: "name", sortable: true, isShowable: true, width: 256 },
      { headerName: "Address", field: "address1", sortable: true, width: 160 },
      { headerName: "City", field: "city", sortable: true, width: 160 },
      { headerName: "Country", field: "countryName", sortable: true, width: 330 },
      { headerName: "Phone", field: "phone", sortable: true, width: 330 },
      { headerName: "Tags", field: "tags", sortable: true, width: 330 },
    ];
  }

  syncwarehouses() {
    this.warehouseService.syncLocations().subscribe(res => {
      console.log(res);
    })
  }

}

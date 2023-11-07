import { Component, OnInit, ViewChild } from '@angular/core';
import { InventoryLevelService } from '../../services/inventory-level.service';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { SearchParams } from 'src/app/core/models/Filters/SearchParams';
import * as moment from "moment";

@Component({
  selector: 'app-inventory-import-list',
  templateUrl: './inventory-import-list.component.html',
  styleUrls: ['./inventory-import-list.component.scss']
})
export class InventoryImportListComponent implements OnInit {
  importFilesColumns: any[] = [];
  searchParams = new SearchParams();
  importFilesData: any[] = [];
  constructor(private inventoryLevelService: InventoryLevelService) { }

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
    this.getImportFiles();
  }

  initInventoryColumns(): void {
    this.importFilesColumns = [
      { headerName: "File Name", field: "fileName", sortable: true, isShowable: true, width: 235 },
      { headerName: "File Type", field: "extension", sortable: true, isShowable: true },
      { headerName: "Status", field: "status", sortable: true, isShowable: true, width: 256 },
      {
        headerName: "Note", field: "note", sortable: true, width: 350,
        wrapText: true,
        autoHeight: true,
      },
      {
        headerName: "Uploaded At", field: "createdAt", sortable: true, width: 180,
        valueFormatter: (params) => {
          let value = params.value;
          let date = moment(value, "dd-MM-yyyy hh:mm:ss");
          if (date.isValid()) {
            value = date.format("dd-MM-yyyy hh:mm:ss");
          }
          return value;
        }
      },
    ];
  }



  getImportFiles() {
    this.searchParams.pageNumber = 0
    this.searchParams.pageSize = 100;
    this.inventoryLevelService.getAlls(this.searchParams).subscribe(res => {
      console.log(res)
      this.importFilesData = res.data;
    })
  }
}

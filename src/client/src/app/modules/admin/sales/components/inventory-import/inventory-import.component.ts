import { Component, OnInit, ViewChild } from '@angular/core';
import { Upload } from 'src/app/core/models/uploads/upload';
import { UploadType } from 'src/app/core/models/uploads/upload-type';
import { CsvMapping, CsvParserService, NgxCSVParserError } from 'src/app/core/services/csv-parser.service';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { InventoryLevelService } from '../../services/inventory-level.service';

@Component({
  selector: 'app-inventory-import',
  templateUrl: './inventory-import.component.html',
  styleUrls: ['./inventory-import.component.scss']
})
export class InventoryImportComponent implements OnInit {
  upload = new Upload();
  csvMapping: CsvMapping[] = [
    {
      csvColumn: "location",
      gridColumn: "location"
    },
    {
      csvColumn: "qty",
      gridColumn: "qty"
    },
    {
      csvColumn: "sku",
      gridColumn: "sku"
    },
    {
      csvColumn: "warehouse",
      gridColumn: "warehouse"
    }
  ];
  inventoryColumns: any[] = [];

  inventoryData: any[] = [];
  constructor(private csvParser: CsvParserService, private inventoryLevelService: InventoryLevelService) { }

  ngOnInit(): void {
    this.initInventoryColumns();
  }

  getWarehouse() {

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

  initInventoryColumns(): void {
    this.inventoryColumns = [
      { headerName: "Location", field: "location", sortable: true, isShowable: true, width: 256 },
      { headerName: "Qty", field: "qty", sortable: true, isShowable: true, width: 256 },
      { headerName: "SKU", field: "sku", sortable: true, width: 160 },
      { headerName: "Warehouse", field: "warehouse", sortable: true, width: 160 },
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

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      this.upload.fileName = event.target.files[0].name.split('.').shift()
      this.upload.extension = event.target.files[0].name.split('.').pop();
      this.upload.uploadType = UploadType.Inventory;

      reader.onloadend = (event) => { // called once readAsDataURL is completed
        this.upload.data = event.target.result;
      }

      this.csvParser
        .parse(event.target.files[0], {
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
    console.log(this.inventoryData)
    var model = {
      UploadRequest: this.upload,
      ImportFile: {
        FileName: this.upload.fileName,
        Extension: this.upload.extension,
        UploadType: this.upload.uploadType,
        ImportRecords: this.inventoryData
      }
    }
    this.inventoryLevelService.ImportFile(model).subscribe(res => {
      console.log(res)
    })
  }
}

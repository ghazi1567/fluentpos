import { Component, OnInit, ViewChild } from '@angular/core';
import { Upload } from 'src/app/core/models/uploads/upload';
import { UploadType } from 'src/app/core/models/uploads/upload-type';
import { CsvMapping, CsvParserService, NgxCSVParserError } from 'src/app/core/services/csv-parser.service';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { InventoryLevelService } from '../../services/inventory-level.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

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
    },
    {
      csvColumn: "rack",
      gridColumn: "rack"
    },
    {
      csvColumn: "IgnoreRackCheck",
      gridColumn: "ignoreRackCheck"
    },
  ];
  inventoryColumns: any[] = [];

  inventoryData: any[] = [];
  warehouseData: any[] = [];
  rowClassRules: any;
  constructor(private csvParser: CsvParserService, private inventoryLevelService: InventoryLevelService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.getLookup();
    this.initInventoryColumns();
  }

  getLookup() {
    this.inventoryLevelService.getWarehouseLookup().subscribe(res => {
      this.warehouseData = res.data;
    });
  }
  onDownloadSampleFile() {
    var url = environment.apiFileUrl + '/sample/sample-inventory.xlsx';
    var win = window.open(url, '_blank');
  }
  gridOptions = []
  private AgGrid: AgGridBaseComponent;
  @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
    if (content) {
      // initially setter gets called with undefined
      this.AgGrid = content;
    }
  }

  gridReady(event): void {
  }

  initInventoryColumns(): void {
    this.inventoryColumns = [
      {
        headerName: "Warehouse", field: "warehouse", sortable: true, width: 200,
        cellClassRules: {
          'valid-row': function (params) { return params.data.isValid == true; },
          'invalid-row': function (params) { return params.data.isValid == false; },
        }
      },
      { headerName: "SKU", field: "sku", sortable: true, width: 256 },
      { headerName: "Qty", field: "qty", sortable: true, isShowable: true, width: 160 },
      { headerName: "Rack", field: "rack", sortable: true, isShowable: true, width: 160 },
      { headerName: "IgnoreRackCheck", field: "ignoreRackCheck", sortable: true, isShowable: true, width: 256 },
      { headerName: "Is Valid", field: "isValid", sortable: true, isShowable: true, width: 256 },

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
    return array.filter(obj => {
      let key = properties.map(prop => obj[prop]).join('|');
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

      this.upload.fileName = event.target.files[0].name.split('.').shift()
      this.upload.extension = event.target.files[0].name.split('.').pop();
      this.upload.uploadType = UploadType.Inventory;

      reader.onloadend = (event) => { // called once readAsDataURL is completed
        this.upload.data = event.target.result;
      }

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
            let uniqueArray = this.removeDuplicates(result, ['sku', 'warehouse', 'rack']);
            uniqueArray.forEach(x => {
              x.isValid = this.warehouseData.find(w => w.code == x.warehouse) != null;
            });
            this.inventoryData = uniqueArray;
            console.log(uniqueArray);
          },
          (error: NgxCSVParserError) => {
            console.log("Error", error);
          }
        );
    }
  }

  saveInventory() {
    console.log(this.inventoryData)

    var isInvalid = this.inventoryData.find(x => x.isValid == false);
    if (isInvalid) {
      this.toastr.error('Highlighted warehouse code not found ');
      this.toastr.error('Please correct warehouse code before saving ');
      return;
    }


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
      if (res.succeeded) {
        this.toastr.success(res.messages[0])
        this.router.navigateByUrl('/admin/sales/inventory-import-files');
      }
      else {
        this.toastr.error(res.messages[0])
      }
    })
  }
}

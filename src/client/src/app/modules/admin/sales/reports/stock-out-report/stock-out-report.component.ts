import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { Result } from "src/app/core/models/wrappers/Result";
import { CsvParserService } from "src/app/core/services/csv-parser.service";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { ProductParams } from "../../../catalog/models/productParams";
import { StockReport } from "../../../catalog/models/stockReport";
import { PurchaseOrderService } from "../../services/purchase-order.service";
import { ReportService } from "../../services/report.service";
import { WarehouseService } from "../../services/warehouse.service";
@Component({
  selector: 'app-stock-out-report',
  templateUrl: './stock-out-report.component.html',
  styleUrls: ['./stock-out-report.component.scss']
})
export class StockOutReportComponent implements OnInit {
  orderColumns: TableColumn[];
  orderParams: any;
  searchString: string;
  productLookups: any[];
  filteredproducts: any[];
  displayedColumns: string[] = ["barcode","productName","location","location2",  "AvailableQuantity"];
  dataSource: Result<StockReport[]>;
  selectedProduct: any = {
      productName: "",
      warehouseId: "",
      startDate: new Date(),
      endDate: new Date()
  };
  warehouseLookups: any[];

  constructor(
      public reportService: ReportService,
      private purchaseOrderApi: PurchaseOrderService,
      public dialog: MatDialog,
      public toastr: ToastrService,
      private warehouseService: WarehouseService,
      private csvParserService: CsvParserService
  ) {}

  ngOnInit(): void {
      this.loadLookups();
  }
  loadLookups() {
      let model = <ProductParams>{
          pageSize: 10000
      };
      this.purchaseOrderApi.getProducts(model).subscribe((res) => {
          this.productLookups = res.data;
      });
      this.warehouseService.getAll().subscribe((res) => {
          this.warehouseLookups = res.data;
      });
  }
  displayProduct(product) {
      if (product) {
          return product.name;
      }
      return "";
  }
  filterProduct(value: any) {
      if (this.selectedProduct.productName) {
          const filterValue = this.selectedProduct.productName.toLowerCase();
          this.filteredproducts = this.productLookups.filter((option) => option.name.toLowerCase().includes(filterValue) || option.barcodeSymbology.toLowerCase().includes(filterValue));
      }
  }
  getOrders(): void {
      this.reportService.getStockOutReport(this.orderParams).subscribe((result) => {
       
          result.data.forEach((x) => {
              var product = this.productLookups.find((obj) => {
                  return obj.id === x.productId;
              });
              if(product){
                  x.productName = product.name;
                  x.barcode = product.barcodeSymbology;
                  x.productCode = product.productCode;
                  x.location = product.location;
                  x.location2 = product.location2;
              }
              
              var warehouse = this.warehouseLookups.find((obj) => {
                  return obj.id === x.warehouseId;
              });
              if (warehouse) {
                  x.warehouseName = warehouse.name;
              }
          });
          this.dataSource = result;
      });
  }

  pageChanged(event: PaginatedFilter): void {
      this.orderParams.pageNumber = event.pageNumber;
      this.orderParams.pageSize = event.pageSize;
      this.getOrders();
  }

  sort($event: Sort): void {
      this.orderParams.orderBy = $event.active + " " + $event.direction;
      console.log(this.orderParams.orderBy);
      this.getOrders();
  }

  filter($event: string): void {
      this.orderParams.searchString = $event.trim().toLocaleLowerCase();
      this.orderParams.pageNumber = 0;
      this.orderParams.pageSize = 0;
      this.getOrders();
  }

  reload(): void {
      this.orderParams.searchString = "";
      this.orderParams.pageNumber = 0;
      this.orderParams.pageSize = 0;
      this.getOrders();
  }
  search(): void {
      // if (!this.selectedProduct.productName || typeof this.selectedProduct.productName !== "object") {
      //     this.toastr.error("Select valid product.");
      //     return;
      // }
      this.orderParams = {};
      this.orderParams.productId = this.selectedProduct.productName.id;
      // this.orderParams.warehouseId = this.selectedProduct.warehouseId;
      this.orderParams.startDate = this.selectedProduct.startDate;
      this.orderParams.endDate = this.selectedProduct.endDate;
      this.getOrders();
  }

  export(){
    let mappedArray = this.dataSource.data.map( (o)=> {
        return {
            Barcode:o.barcode,
            ProductCode: o.productCode,
            ProductName: o.productName,
            Location : o.location,
            Location2 : o.location2,
            Quantity: o.availableQuantity
        }
    });
    this.csvParserService.exportXls( mappedArray,'DailyReport.xlsx','Stock Out');
  }
}

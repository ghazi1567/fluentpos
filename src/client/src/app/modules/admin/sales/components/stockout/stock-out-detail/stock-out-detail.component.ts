import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/modules/admin/catalog/models/product';
import { ProductParams } from 'src/app/modules/admin/catalog/models/productParams';
import { ProductService } from 'src/app/modules/admin/catalog/services/product.service';
import { Order } from '../../../models/order';
import { StockInService } from '../../../services/stock-in.service';

@Component({
  selector: 'app-stock-out-detail',
  templateUrl: './stock-out-detail.component.html',
  styleUrls: ['./stock-out-detail.component.scss']
})
export class StockOutDetailComponent implements OnInit {
  order: Order;
  products: Product[];
  displayedColumns: string[] = ["barcodeSymbology", "productName",  "quantity"];
  constructor(@Inject(MAT_DIALOG_DATA) public data: Order, public stockInService: StockInService, private toastr: ToastrService, private productApi: ProductService) {}

  ngOnInit(): void {
      this.getOrder();
  }
  getOrder() {
      this.stockInService.getById(this.data.id).subscribe((response) => {
          this.order = response.data;
      });
  }
  getProducts() {
      let params = new ProductParams();
      params.pageSize = 10000;
      this.productApi.getProducts(params).subscribe((res) => {
          this.products = res.data;
      });
  }

  getProductName(id) {
      console.log(this.products);
      let product = this.products.find((x) => x.id == id);
      if (product) {
          return product.name;
      }
      return "";
  }

  getProductBarcode(id) {
      console.log(this.products);
      let product = this.products.find((x) => x.id == id);
      if (product) {
          return product.barcodeSymbology;
      }
      return "";
  }
}

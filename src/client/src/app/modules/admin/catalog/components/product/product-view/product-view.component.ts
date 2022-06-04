import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Upload } from "src/app/core/models/uploads/upload";
import { UploadType } from "src/app/core/models/uploads/upload-type";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { Brand } from "../../../models/brand";
import { BrandParams } from "../../../models/brandParams";
import { Category } from "../../../models/category";
import { CategoryParams } from "../../../models/categoryParams";
import { Product } from "../../../models/product";
import { BrandService } from "../../../services/brand.service";
import { CategoryService } from "../../../services/category.service";
import { ProductService } from "../../../services/product.service";

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.scss']
})
export class ProductViewComponent implements OnInit {
  productForm: FormGroup;
  formTitle: string;
  brands: PaginatedResult<Brand>;
  brandParams = new BrandParams();
  categories: PaginatedResult<Category>;
  categoryParams = new CategoryParams();

  url: any = [];
  upload = new Upload();

  constructor(
      @Inject(MAT_DIALOG_DATA) public data: Product,
      private productService: ProductService,
      private brandService: BrandService,
      private categoryService: CategoryService,
      private toastr: ToastrService,
      private fb: FormBuilder
  ) {}

  ngOnInit(): void {
      this.initializeForm();
      this.brandParams.pageSize = 50;
      this.categoryParams.pageSize = 50;
      this.getBrands();
      this.getCategories();
      this.loadProductImage();
  }

  initializeForm() {
      this.productForm = this.fb.group({
          id: [this.data && this.data.id],
          name: [this.data && this.data.name, Validators.required],
          price: [this.data && this.data.price, Validators.required],
          cost: [this.data && this.data.cost, Validators.required],
          barcodeSymbology: [this.data && this.data.barcodeSymbology, Validators.required],
          detail: [this.data && this.data.detail],
          productCode: [this.data && this.data.productCode],
          location: [this.data && this.data.location],
          discountFactor: [this.data && this.data.discountFactor],
      });
      this.formTitle = "View Product";
      this.productForm.disable();
  }

  getBrands() {
      this.brandService.getBrands(this.brandParams).subscribe((response) => {
          this.brands = response;
      });
  }

  getCategories() {
      this.categoryService.getCategories(this.categoryParams).subscribe((response) => {
          this.categories = response;
      });
  }

  loadProductImage() {
      if (this.validURL(this.data.imageUrl)) {
          this.url = this.data.imageUrl;
          return;
      }
      this.productService.getProductImageById(this.data.id).subscribe((response) => {
          this.url = response.data;
      });
  }

  onSelectFile($event) {
      this.upload = $event;
  }

  onSubmit() {
      // TODO after successful update/insert, refresh table view in component product.component.ts

      if (this.productForm.valid) {
          if (this.productForm.get("id").value === "" || this.productForm.get("id").value == null) {
              this.productService.createProduct(this.productForm.value, this.upload).subscribe((response) => {
                  this.toastr.success(response.messages[0]);
              });
          } else {
              this.productService.updateProduct(this.productForm.value, this.upload).subscribe((response) => {
                  this.toastr.success(response.messages[0]);
              });
          }
      }
  }

  validURL(str) {
      var pattern = new RegExp(
          "^(https?:\\/\\/)?" + // protocol
              "((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|" + // domain name
              "((\\d{1,3}\\.){3}\\d{1,3}))" + // OR ip (v4) address
              "(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*" + // port and path
              "(\\?[;&a-z\\d%_.~+=-]*)?" + // query string
              "(\\#[-a-z\\d_]*)?$",
          "i"
      ); // fragment locator
      return !!pattern.test(str);
  }
}

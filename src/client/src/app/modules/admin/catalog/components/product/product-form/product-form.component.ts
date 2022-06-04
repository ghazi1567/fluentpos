import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { Upload } from "src/app/core/models/uploads/upload";
import { UploadType } from "src/app/core/models/uploads/upload-type";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { WarehouseService } from "src/app/modules/admin/sales/services/warehouse.service";
import { Brand } from "../../../models/brand";
import { BrandParams } from "../../../models/brandParams";
import { Category } from "../../../models/category";
import { CategoryParams } from "../../../models/categoryParams";
import { Product } from "../../../models/product";
import { BrandService } from "../../../services/brand.service";
import { CategoryService } from "../../../services/category.service";
import { ProductService } from "../../../services/product.service";

@Component({
    selector: "app-product-form",
    templateUrl: "./product-form.component.html",
    styleUrls: ["./product-form.component.scss"]
})
export class ProductFormComponent implements OnInit {
    productForm: FormGroup;
    formTitle: string;
    brands: PaginatedResult<Brand>;
    brandParams = new BrandParams();
    categories: PaginatedResult<Category>;
    categoryParams = new CategoryParams();

    url: any = [];
    upload = new Upload();
    warehouseLookups: any[];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: Product,
        private productService: ProductService,
        private brandService: BrandService,
        private categoryService: CategoryService,
        private toastr: ToastrService,
        private warehouseService: WarehouseService,
        private fb: FormBuilder
    ) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
        this.brandParams.pageSize = 50;
        this.categoryParams.pageSize = 50;
        this.getBrands();
        this.getCategories();
        this.loadProductImage();
    }
    loadLookups() {
        this.warehouseService.getAll().subscribe((res) => {
            this.warehouseLookups = res.data;
        });
    }
    initializeForm() {
        this.productForm = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            // brandId: [this.data && this.data.brandId, Validators.required], // todo get brands and show dropdown to select brand instead of hidden input
            // categoryId: [this.data && this.data.categoryId, Validators.required], // todo get categories and show dropdown list to select category
            // localeName: [this.data && this.data.localeName],
            price: [this.data && this.data.price, Validators.required],
            cost: [this.data && this.data.cost, Validators.required],
            // tax: [this.data && this.data.tax],
            // taxMethod: [this.data && this.data.taxMethod],
            barcodeSymbology: [this.data && this.data.barcodeSymbology, Validators.required],
            // isAlert: [!!(this.data && this.data.isAlert)],
            // alertQuantity: [this.data && this.data.alertQuantity],
            detail: [this.data && this.data.detail],
            productCode: [this.data && this.data.productCode],
            location: [this.data && this.data.location],
            discountFactor: [this.data && this.data.discountFactor],
            imageUrl: [this.data && this.data.imageUrl],
            warehouseId: [this.data && this.data.warehouseId],
            openingStock: [this.data && this.data.openingStock],
        });
        if (this.productForm.get("id").value === "" || this.productForm.get("id").value == null) {
            this.formTitle = "Register Product";
        } else {
            this.formTitle = "Edit Product";
        }
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
                let model = this.productForm.value;
                model.brandId = "370b8ad8-dfb1-45a4-a04d-e60d0ca97060";
                model.categoryId = "c553786e-b8c5-4ff8-823d-120b35210a29";

                this.productService.createProduct(model, this.upload).subscribe(
                    (response) => {
                        if (response.succeeded) {
                            this.toastr.success(response.messages[0]);
                        } else {
                            response.messages.forEach((element) => {
                                this.toastr.error(element);
                            });
                        }
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.error(element);
                        });
                    }
                );
            } else {
                let model = this.productForm.value;
                model.brandId = "370b8ad8-dfb1-45a4-a04d-e60d0ca97060";
                model.categoryId = "c553786e-b8c5-4ff8-823d-120b35210a29";

                this.productService.updateProduct(model, this.upload).subscribe(
                    (response) => {
                        if (response.succeeded) {
                            this.toastr.success(response.messages[0]);
                        } else {
                            response.messages.forEach((element) => {
                                this.toastr.error(element);
                            });
                        }
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.error(element);
                        });
                    }
                );
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
    onUrlChange(url) {
        this.url = url;
    }
}

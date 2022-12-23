import { Component, OnInit } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { MatTableDataSource } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { ProductParams } from "../../../catalog/models/productParams";
import { Order } from "../../models/order";
import { Product } from "../../models/product";
import { PurchaseOrderService } from "../../services/purchase-order.service";
import { WarehouseService } from "../../services/warehouse.service";

@Component({
    selector: "app-purchase-order",
    templateUrl: "./purchase-order.component.html",
    styleUrls: ["./purchase-order.component.scss"]
})
export class PurchaseOrderComponent implements OnInit {
    displayedColumns: string[] = ["barcodeSymbology", "productName", "quantity", "price", "total", "action"];
    products: Product[];
    dataSource = new MatTableDataSource<Product>();
    orderForm: UntypedFormGroup;
    defaultProduct: Product = {
        productId: "",
        quantity: 1,
        price: 0,
        total: 0,
        brand: "",
        category: "",
        discount: 0,
        orderId: "",
        tax: 0,
        productName: ""
    };
    selectedProduct: Product = {
        productId: "",
        quantity: 1,
        price: 0,
        total: 0,
        brand: "",
        category: "",
        discount: 0,
        orderId: "",
        tax: 0,
        productName: ""
    };
    productLookups: any[];
    warehouseLookups: any[];
    filteredproducts: any[];
    constructor(private toastr: ToastrService, 
        private fb: UntypedFormBuilder, 
        private purchaseOrderApi: PurchaseOrderService,
        private warehouseService : WarehouseService) {}

    ngOnInit(): void {
        this.loadLookups();
        this.initializeForm();
    }
    loadLookups() {
        let model = <ProductParams>{
            pageSize: 10000
        };
        this.purchaseOrderApi.getProducts(model).subscribe((res) => {
            this.productLookups = res.data;
        });
        this.warehouseService.getAll().subscribe(res=>{
            this.warehouseLookups =res.data;
        })
    }
    initializeForm() {
        this.orderForm = this.fb.group({
            id: [""],
            referenceNumber: ["", Validators.required],
            comments: [""],
            warehouseId: ["",Validators.required],
        });
    }
    filter(values) {
        return values.filter((x) => x.name.toLowerCase().includes(this.selectedProduct.productId));
    }
    addProduct(product: any) {
        if (!this.products) {
            this.products = [];
        }

        if (product.productName && product.quantity) {
            var exist = this.products.filter((x) => x.productId == product.productName.id);
            if (exist.length == 0) {
                let newProduct = <Product>{
                    brand: product.brand,
                    category: product.brand,
                    discount: product.discount,
                    price: product.price,
                    quantity: product.quantity,
                    total: product.total,
                    productId: product.productName.id,
                    productName: product.productName.name,
                    barcodeSymbology: product.productName.barcodeSymbology
                };
                this.dataSource.data.push(newProduct);
                this.dataSource._updateChangeSubscription();
                Object.assign(this.selectedProduct, this.defaultProduct);
            } else {
                this.toastr.error("Product already exist");
            }
        }
    }

    removeProduct1(item: any) {
        const index = this.dataSource.data.indexOf(item.barcodeSymbology);
        this.dataSource.data.splice(index, 1);
        this.dataSource._updateChangeSubscription(); // <-- Refresh the datasource
    }
    removeProduct(item: any) {
        const data = this.dataSource.data;
        let index: number = data.findIndex((d) => d === item);
        data.splice(index, 1);
        this.dataSource.data = data;
    }
    savePurchaseOrder() {
        var formValues = this.orderForm.value;
        console.log(formValues);
        let order = <Order>{
            id: "00000000-0000-0000-0000-000000000000",
            products: this.dataSource.data,
            referenceNumber: formValues.referenceNumber,
            note: formValues.comments,
            warehouseId : formValues.warehouseId
        };
        console.log(order);
        if(order.products.length == 0){
            this.toastr.error('Please select valid products');
            return;
        }
        this.purchaseOrderApi.createPO(order).subscribe((res) => {
            console.log(res);
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.dataSource.data = [];
                this.dataSource._updateChangeSubscription(); 
                this.initializeForm();
            }
        });
    }
    onSubmit() {}
    filterProduct(value: any) {
        const filterValue = this.selectedProduct.productName.toLowerCase();
        this.filteredproducts = this.productLookups.filter((option) => option.name.toLowerCase().includes(filterValue) || option.barcodeSymbology.toLowerCase().includes(filterValue));
        console.log(this.filteredproducts);
    }
    displayProduct(product) {
        if (product) {
            return product.name;
        }
        return "";
    }
    onProductSelection(value) {
        console.log(value);
        this.selectedProduct.price = value.price;
        this.calculateRowTotal();
    }
    calculateRowTotal() {
        this.selectedProduct.total = this.selectedProduct.price * this.selectedProduct.quantity;
    }
}

import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { ProductParams } from "src/app/modules/admin/catalog/models/productParams";
import { Order } from "../../../models/order";
import { Product } from "../../../models/product";
import { PurchaseOrderService } from "../../../services/purchase-order.service";
import { StockInService } from "../../../services/stock-in.service";
import { WarehouseService } from "../../../services/warehouse.service";

@Component({
    selector: "app-stock-in",
    templateUrl: "./stock-in.component.html",
    styleUrls: ["./stock-in.component.scss"]
})
export class StockInComponent implements OnInit {
    displayedColumns: string[] = ["barcodeSymbology", "productName", "quantity", "price", "total", "action"];
    products: Product[];
    dataSource = new MatTableDataSource<Product>();
    orderForm: FormGroup;
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
    poLookups: any[];
    poFilteredOptions: Observable<any[]>;
    filteredproducts: any[];
    warehouseLookups: any[];
    constructor(private toastr: ToastrService, 
        private fb: FormBuilder, 
        private stockInService: StockInService, 
        private purchaseOrderApi: PurchaseOrderService,
        private warehouseService : WarehouseService) {}

    ngOnInit(): void {
        this.initializeForm();
        this.loadLookups();
    }
    loadLookups() {
        let model = <ProductParams>{
            pageSize: 10000
        };
        this.purchaseOrderApi.getProducts(model).subscribe((res) => {
            this.productLookups = res.data;
        });
        this.stockInService.getPurchaseOrders().subscribe((res) => {
            this.poLookups = res.data;
            this.poFilteredOptions = this.orderForm.get("referenceNumber").valueChanges.pipe(
                startWith(""),
                map((value) => (typeof value === "object" ? this._filter(value.value) : this._filter(value)))
            );
        });
        this.warehouseService.getAll().subscribe(res=>{
            this.warehouseLookups =res.data;
        });
    }
    initializeForm() {
        this.orderForm = this.fb.group({
            id: [""],
            referenceNumber: ["", Validators.required],
            timeStamp: [new Date(), Validators.required],
            comments: [""],
            warehouseId: ["",Validators.required],
        });
    }
    private _filter(value: string): any[] {
        if (!value || !this.poLookups) {
            return [];
        }
        const filterValue = value.toLowerCase();
        console.log(this.poLookups);
        return this.poLookups.filter((option) => option.referenceNumber && option.referenceNumber.toLowerCase().includes(filterValue));
    }
    filter(values) {
        return values.filter((x) => x.name.toLowerCase().includes(this.selectedProduct.productId));
    }

    onReferenceSelection(event) {
        console.log(event);
        this.purchaseOrderApi.getById(event.option.value.id).subscribe((res) => {
            if (res.succeeded) {
                this.products = res.data.products;
                this.products.forEach((x) => {
                    x.productName = this.getProductName(x.productId);
                    x.barcodeSymbology = this.getProductBarcode(x.productId);
                });
                this.dataSource.data = this.products;
                this.dataSource._updateChangeSubscription();
            }
        });
    }

    getProductName(id) {
        console.log(this.productLookups);
        let product = this.productLookups.find((x) => x.id == id);
        if (product) {
            return product.name;
        }
        return "";
    }

    getProductBarcode(id) {
        console.log(this.productLookups);
        let product = this.productLookups.find((x) => x.id == id);
        if (product) {
            return product.barcodeSymbology;
        }
        return "";
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
            id: "",
            products: this.dataSource.data,
            referenceNumber: formValues.referenceNumber.referenceNumber,
            note: formValues.comments,
            timeStamp: new Date(formValues.timeStamp),
            warehouseId : formValues.warehouseId
        };
        if(order.products.length == 0){
            this.toastr.error('Please select valid products');
            return;
        }
        this.stockInService.create(order).subscribe((res) => {
            if (res.succeeded) {
                this.toastr.success(res.messages[0]);
                this.dataSource.data = [];
                this.dataSource._updateChangeSubscription(); // <-- Refresh the datasource
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
    filterOrder(value: any) {
        const filterValue = this.selectedProduct.productName.toLowerCase();
        this.filteredproducts = this.poLookups.filter((option) => option.name.toLowerCase().includes(filterValue) || option.barcodeSymbology.toLowerCase().includes(filterValue));
        console.log(this.filteredproducts);
    }
    displayProduct(product) {
        if (product) {
            return product.name;
        }
        return "";
    }
    displayReference(ref) {
        if (ref) {
            return ref.referenceNumber;
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
    calculateGridRowTotal(row) {
        row.total = row.price * row.quantity;
    }

    onPageChange(pageEvent: PageEvent) {
        const event: PaginatedFilter = {
            pageNumber: pageEvent.pageIndex + 1 ?? 1,
            pageSize: pageEvent.pageSize ?? 10
        };
        console.log(event);
        // this.onPageChanged.emit(event);
    }
}

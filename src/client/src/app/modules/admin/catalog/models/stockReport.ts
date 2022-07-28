export interface StockReport {
    productName: string;
    warehouseName: string;
    availableQuantity: number;
    lastUpdatedOn: string;
    name: string;
    productId: string;
    warehouseId: string;
    barcode?:string;
    productCode?:string;
    location?:string;
    location2?:string;
}

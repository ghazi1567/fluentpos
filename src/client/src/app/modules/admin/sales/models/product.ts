export interface Product {
  orderId: string;
  productId: string;
  productName?: string;
  quantity: number;
  category: string;
  brand: string;
  price: number;
  tax: number;
  discount: number;
  total: number;
  barcodeSymbology?:string;
  productCode?:string;
}

import { OrderType } from "src/app/core/enums/OrderType";
import { Product } from "./product";

export interface Order {
    id: string;
    referenceNumber?: string;
    timeStamp: Date;
    customerId?: string;
    customerName?: string;
    customerPhone?: string;
    customerEmail?: string;
    subTotal?: number;
    tax?: number;
    discount?: number;
    total: number;
    isPaid?: boolean;
    note?: string;
    products: Product[];
    orderType: OrderType;
    status: number;
    warehouseId:string;
}

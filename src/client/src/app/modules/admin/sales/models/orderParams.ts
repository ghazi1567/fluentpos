import { OrderType } from "src/app/core/enums/OrderType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";

export class OrderParams implements PaginatedFilter {
    searchString: string;
    isApproved?: string;
    pageNumber: number;
    pageSize: number;
    orderBy: string;
    status?: number;
    orderType?: OrderType;
}

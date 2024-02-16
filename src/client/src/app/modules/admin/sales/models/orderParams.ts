import { OrderType } from "src/app/core/enums/OrderType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { FilterModel, SortModel } from "src/app/core/shared/components/ag-grid-base/ag-grid.models";

export class OrderParams implements PaginatedFilter {
    searchString: string;
    isApproved?: string;
    pageNumber: number;
    pageSize: number;
    orderBy: string;
    status?: number;
    orderType?: OrderType;
    bypassCache: boolean;
    advanceFilters?: FilterModel[];
    sortModel?: SortModel[];
    AdvancedSearchType?: "And" | "Or";
}

import { FilterModel } from "../../shared/components/ag-grid-base/ag-grid.models";

export interface PaginatedFilter {
    pageNumber: number;
    pageIndex?: number;
    pageSize: number;
    advanceFilters?: FilterModel[];
    AdvancedSearchType?: "And" | "Or";
}

import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { FilterModel, SortModel } from 'src/app/core/shared/components/ag-grid-base/ag-grid.models';

export class ProductParams implements PaginatedFilter {
  searchString: string;
  brandId: number;
  categoryId: number;
  pageNumber: number;
  pageSize: number;
  orderBy: string;
  bypassCache: boolean;
  advanceFilters?: FilterModel[];
  sortModel?: SortModel[];
  AdvancedSearchType?: "And" | "Or";
}

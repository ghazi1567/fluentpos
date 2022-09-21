import { PaginatedFilter } from "./PaginatedFilter";

export class SearchParams implements PaginatedFilter {
    searchString: string;
    pageNumber: number;
    pageSize: number;
    orderBy: string;
}

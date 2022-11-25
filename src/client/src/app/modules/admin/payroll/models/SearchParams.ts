import { SalaryPerksType } from "src/app/core/enums/SalaryPerksType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";

export class SearchParams implements PaginatedFilter {
    searchString: string;
    pageNumber: number;
    pageSize: number;
    orderBy: string;
    employeeId: string;
    type: SalaryPerksType;
}

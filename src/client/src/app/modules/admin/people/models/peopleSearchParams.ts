import { RequestType } from "src/app/core/enums/RequestType";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";

export class PeopleSearchParams implements SearchParams {
    searchString: string;
    pageNumber: number;
    pageSize: number;
    orderBy: string;
    employeeId?: string;
    requestId?: string;
    requestType?: RequestType;
    
}

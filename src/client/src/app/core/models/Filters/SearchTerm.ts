export interface NgAsSearchTerm {
    searchMode: "simple" | "advanced";

    simpleSearchTerm: string;

    advancedSearchType: "and" | "or";
    advancedTerms: NgAsAdvancedSearchTerm[];

    name?: string;
    isDefault?: boolean;
}

export interface NgAsAdvancedSearchTerm {
    id: number;
    fieldName?: string;
    fieldType?: "text" | "date" | "email" | "number" | "search" | "tel" | "url" | "time" | "checkbox" | "dropdown";
    action?: "contains" | "equals" | "larger than" | "smaller than" | "=" | ">=" | "<=";
    searchTerm?: string;
    isNegated?: boolean;
    data?: Lookup[];
}

export interface NgAsHeader {
    id: string;
    displayText: string;
    type?: "text" | "date" | "email" | "number" | "search" | "tel" | "url" | "time" | "checkbox" | "dropdown";
    data?: Lookup[];
}

export interface NgAsConfig {
    headers: NgAsHeader[];
    simpleFieldLabel?: string;
    defaultTerm?: NgAsSearchTerm;
    inputArray?: any[];
    showFilterSaving?: boolean;
    savedFilters?: NgAsSearchTerm[];
}

export interface Lookup {
    key: string;
    value: string;
}

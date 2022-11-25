export interface AdvanceFilter {
    key: string;
    name: string;
    label: string;
    value?: any;
    controlType: "text" | "password" | "email" | "number" | "search" | "tel" | "url" | "time" | "checkbox" | "dropdown";
    data?: Lookup[];
}

export interface Lookup {
    key: string;
    value: string;
}

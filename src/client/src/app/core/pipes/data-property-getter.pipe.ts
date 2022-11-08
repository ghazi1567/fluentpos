import { Pipe, PipeTransform } from "@angular/core";
import { DatePipe, CurrencyPipe } from "@angular/common";
@Pipe({
    name: "dataPropertyGetter"
})
export class DataPropertyGetterPipe implements PipeTransform {
    constructor(private datePipe: DatePipe, private currencyPipe: CurrencyPipe) {}

    transform(object: any, column: any, ...args: unknown[]): unknown {
        if (column["columnType"] == "date" || column["columnType"] == "time") {
            return this.datePipe.transform(object[column.dataKey], column["format"]);
        } else if (column["columnType"] == "bool") {
            return object[column.dataKey] == true ? "Yes" : "No";
        } else if (column["columnType"] == "currency") {
            return this.currencyPipe.transform(object[column.dataKey]);
        } else {
            return object[column.dataKey];
        }
    }
}

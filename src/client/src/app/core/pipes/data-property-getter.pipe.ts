import { Pipe, PipeTransform } from "@angular/core";
import { DatePipe } from "@angular/common";
@Pipe({
    name: "dataPropertyGetter"
})
export class DataPropertyGetterPipe implements PipeTransform {
    constructor(private datePipe: DatePipe) {}

    transform(object: any, column: any, ...args: unknown[]): unknown {
        if (column["columnType"] == "date" || column["columnType"] == "time") {
            return this.datePipe.transform(object[column.dataKey], column["format"]);
        } else {
            return object[column.dataKey];
        }
    }
}

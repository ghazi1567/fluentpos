import { Injectable } from "@angular/core";
import { Observable, Observer } from "rxjs";
import { Employee } from "src/app/modules/admin/people/models/employee";
import * as XLSX from "XLSX";

@Injectable({
    providedIn: "root"
})
export class CsvParserService {
    private defaultCSVParserConfig = {
        header: true,
        delimiter: ","
    };

    parse(csvFile: File, config: CSVParserConfig): Observable<Array<any> | NgxCSVParserError> {
        config = {
            ...this.defaultCSVParserConfig,
            ...config
        };

        const ngxCSVParserObserver = new Observable((observer: Observer<Array<any> | NgxCSVParserError>) => {
            try {
                let csvRecords = null;

                if (this.isCSVFile(csvFile)) {
                    const reader = new FileReader();
                    reader.readAsText(csvFile);

                    reader.onload = () => {
                        const csvData = reader.result;
                        const csvRecordsArray = this.csvStringToArray((csvData as string).trim(), config.delimiter);

                        const headersRow = this.getHeaderArray(csvRecordsArray);

                        csvRecords = this.getDataRecordsArrayFromCSVFile(csvRecordsArray, headersRow.length, config);

                        observer.next(csvRecords);
                        observer.complete();
                    };

                    reader.onerror = () => {
                        this.badCSVDataFormatErrorHandler(observer);
                    };
                } else {
                    this.notCSVFileErrorHandler(observer);
                }
            } catch (error) {
                this.unknownCSVParserErrorHandler(observer);
            }
        });

        return ngxCSVParserObserver;
    }

    csvStringToArray(csvDataString: string, delimiter: string) {
        const regexPattern = new RegExp(`(\\${delimiter}|\\r?\\n|\\r|^)(?:\"((?:\\\\.|\"\"|[^\\\\\"])*)\"|([^\\${delimiter}\"\\r\\n]*))`, "gi");
        let matchedPatternArray = regexPattern.exec(csvDataString);
        const resultCSV = [[]];
        while (matchedPatternArray) {
            if (matchedPatternArray[1].length && matchedPatternArray[1] !== delimiter) {
                resultCSV.push([]);
            }
            const cleanValue = matchedPatternArray[2] ? matchedPatternArray[2].replace(new RegExp('[\\\\"](.)', "g"), "$1") : matchedPatternArray[3];
            resultCSV[resultCSV.length - 1].push(cleanValue);
            matchedPatternArray = regexPattern.exec(csvDataString);
        }
        return resultCSV;
    }

    getDataRecordsArrayFromCSVFile(csvRecordsArray: any, headerLength: any, config: any) {
        const dataArr = [];
        const headersArray = csvRecordsArray[0];

        const startingRowToParseData = config.header ? 1 : 0;

        for (let i = startingRowToParseData; i < csvRecordsArray.length; i++) {
            const data = csvRecordsArray[i];

            if (data.length === headerLength && config.header) {
                const csvRecord = {};

                for (let j = 0; j < data.length; j++) {
                    let headerName = this.getHeaderMapping(config, headersArray[j]);
                    if (headerName) {
                        if (data[j] === undefined || data[j] === null) {
                            csvRecord[headerName] = "";
                        } else {
                            csvRecord[headerName] = data[j].trim();
                        }
                    }
                }
                dataArr.push(csvRecord);
            } else {
                dataArr.push(data);
            }
        }
        return dataArr;
    }

    isCSVFile(file: any) {
        return file.name.toLowerCase().endsWith(".csv");
    }
    isXlsxFile(file: any) {
        return file.name.toLowerCase().endsWith(".xlsx");
    }
    getHeaderMapping(config: any, headerName: any) {
        if (config.mapping && config.mapping.length > 0) {
            var colMap = config.mapping.find((x) => x.csvColumn.toLowerCase() == headerName.toLowerCase());
            if (colMap) return colMap.gridColumn;
            return "";
        }
        return headerName
            .replace(/\s/g, "")
            .replace(/[~`!@#$%^&*()+={}\[\];:\'\"<>.,\/\\\?-]/g, "")
            .toLowerCase();
    }
    getDefaultValue(config: any, headerName: any) {
        if (config.mapping && config.mapping.length > 0) {
            var colMap = config.mapping.find((x) => x.csvColumn.toLowerCase() == headerName.toLowerCase());
            if (colMap) return colMap.defaultValue;
            return "";
        }
        return "";
    }

    getHeaderArray(csvRecordsArr: any) {
        const headers = csvRecordsArr[0];
        const headerArray = [];
        for (const header of headers) {
            headerArray.push(header);
        }
        return headerArray;
    }

    notCSVFileErrorHandler(observer: Observer<any>) {
        const ngcCSVParserError: NgxCSVParserError = this.errorBuilder("NOT_A_CSV_FILE", "Selected file is not a csv File Type.", 2);
        observer.error(ngcCSVParserError);
    }
    notXLSXFileErrorHandler(observer: Observer<any>) {
        const ngcCSVParserError: NgxCSVParserError = this.errorBuilder("NOT_A_xlsx_FILE", "Selected file is not a xlsx File Type.", 2);
        observer.error(ngcCSVParserError);
    }

    unknownCSVParserErrorHandler(observer: Observer<any>) {
        const ngcCSVParserError: NgxCSVParserError = this.errorBuilder("UNKNOWN_ERROR", "Unknown error. Please refer to official documentation for library usage.", 404);
        observer.error(ngcCSVParserError);
    }

    badCSVDataFormatErrorHandler(observer: Observer<any>) {
        const ngcCSVParserError: NgxCSVParserError = this.errorBuilder("BAD_CSV_DATA_FORMAT", "Unable to parse CSV File.", 1);
        observer.error(ngcCSVParserError);
    }

    errorBuilder(type: string, message: any, code: any): NgxCSVParserError {
        const ngcCSVParserError: NgxCSVParserError = new NgxCSVParserError();
        ngcCSVParserError.type = type;
        ngcCSVParserError.message = message;
        ngcCSVParserError.code = code;
        return ngcCSVParserError;
    }

    parseXlsx(xlsxFile: File, config: CSVParserConfig): Observable<Array<any> | NgxCSVParserError> {
        config = {
            ...this.defaultCSVParserConfig,
            ...config
        };

        const ngxCSVParserObserver = new Observable((observer: Observer<Array<any> | NgxCSVParserError>) => {
            try {
                let arrayBuffer: any;
                let csvRecords = null;

                if (this.isXlsxFile(xlsxFile)) {
                    let fileReader = new FileReader();
                    fileReader.readAsArrayBuffer(xlsxFile);
                    fileReader.onload = (e) => {
                        arrayBuffer = fileReader.result;
                        var data = new Uint8Array(arrayBuffer);
                        var arr = new Array();
                        for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
                        var bstr = arr.join("");
                        var workbook = XLSX.read(bstr, { type: "binary" });
                        var first_sheet_name = workbook.SheetNames[0];
                        var worksheet = workbook.Sheets[first_sheet_name];
                        console.log(XLSX.utils.sheet_to_json(worksheet, { raw: false, dateNF: "yyyy-MM-dd" }));
                        var arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: false, defval: null, dateNF: "yyyy-MM-dd" });
                        var csvRecords = this.renameHeaderKeys(arraylist, config);
                        observer.next(csvRecords);
                        observer.complete();
                    };

                    fileReader.onerror = () => {
                        this.badCSVDataFormatErrorHandler(observer);
                    };
                } else {
                    this.notCSVFileErrorHandler(observer);
                }
            } catch (error) {
                this.unknownCSVParserErrorHandler(observer);
            }
        });

        return ngxCSVParserObserver;
    }
    renameHeaderKeys(data: any[], config: CSVParserConfig) {
        data.forEach((obj) => {
            config.mapping.forEach((map) => {
                var key = map.csvColumn;
                let headerName = this.getHeaderMapping(config, key);
                if (headerName) {
                    if (obj[key] === undefined || obj[key] === null) {
                        obj[headerName] = this.getDefaultValue(config, key);
                    } else {
                        obj[headerName] = obj[key];
                        if (headerName != key) {
                            delete obj[key];
                        }
                    }
                }
            });
        });
        return data;
    }

    exportXls(data: any[], fileName, sheetName) {
        var worksheet = XLSX.utils.json_to_sheet(data);
        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, worksheet, sheetName);
        XLSX.writeFile(wb, fileName);
    }
}

class CSVParserConfig {
    header?: boolean;
    delimiter?: string;
    mapping?: CsvMapping[];

    constructor() {}
}
export class CsvMapping {
    csvColumn: string;
    gridColumn: string;
    defaultValue?: string;
}
export class NgxCSVParserError {
    type: string; // A generalization of the error
    code: number; // Standardized error code
    message: string; // Human-readable details
}

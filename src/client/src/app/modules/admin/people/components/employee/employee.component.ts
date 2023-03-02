import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Sort } from "@angular/material/sort";
import { ToastrService } from "ngx-toastr";
import { AdvanceFilter } from "src/app/core/models/Filters/AdvanceFilter";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { NgAsConfig, NgAsSearchTerm } from "src/app/core/models/Filters/SearchTerm";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { AuthService } from "src/app/core/services/auth.service";
import { CsvMapping, CsvParserService, NgxCSVParserError } from "src/app/core/services/csv-parser.service";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { SearchParams } from "../../../org/models/SearchParams";
import { Employee } from "../../models/employee";
import { EmployeeService } from "../../services/employee.service";
import { EmployeeFormComponent } from "./employee-form/employee-form.component";

@Component({
    selector: "app-employee",
    templateUrl: "./employee.component.html",
    styleUrls: ["./employee.component.scss"]
})
export class EmployeeComponent implements OnInit {
    @Input() activeEmployee: boolean = true;
    employees: PaginatedResult<Employee>;
    employeeColumns: TableColumn[];
    customerParams = new SearchParams();
    searchString: string;
    @ViewChild("file") fileInput: ElementRef;
    csvMapping: CsvMapping[];
    advanceFilters: AdvanceFilter[];
    advanceSearch: NgAsConfig;
    savedFilters: NgAsSearchTerm[];
    policies: any[];
    departments: any[];
    designations: any[];
    constructor(public employeeService: EmployeeService, private authService: AuthService, private csvParser: CsvParserService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.initColumns();
        this.loadLookups();
    }
    loadLookups() {
        let parms = new SearchParams();
        parms.pageSize = 1000;
        // this.employeeService.getPolicyLookup(parms).subscribe((res) => {
        //     this.policies = res.data;
        // });
        this.employeeService.getDepartmentLookup(parms).subscribe((res) => {
            this.departments = res.data;
            this.initAdvanceFilters();
            this.getEmployees();
        });
        // this.employeeService.getDesignationLookup(parms).subscribe((res) => {
        //     this.designations = res.data;
        //     this.filteredDesignations = this.designations.filter((x) => x.departmentId == this.data.departmentId);
        // });
        // this.employeeService.getEmployees(parms).subscribe((res) => {
        //     this.employeesLookup = res.data.filter((x) => x.id != this.data.id);
        // });
    }
    initAdvanceFilters() {
        var departmentsLookupData = this.departments.map((emp) => ({
            key: emp.id,
            value: emp.name
        }));
        this.savedFilters = [];
        this.advanceSearch = {
            headers: [
                { id: "FullName", displayText: "Name" },
                { id: "FatherName", displayText: "Father Name" },
                { id: "MotherName", displayText: "Mother Name" },
                { id: "EmployeeCode", displayText: "Employee Code" },
                { id: "DepartmentId", displayText: "Department", type: "dropdown", data: departmentsLookupData },
                { id: "PunchCode", displayText: "Punch Code", type: "number" },
                { id: "MobileNo", displayText: "Mobile#" },
                { id: "PhoneNo", displayText: "Phone#" },
                { id: "Gender", displayText: "Gender" },
                { id: "CnicNo", displayText: "Cnic#" },
                {
                    id: "EmployeeStatus",
                    displayText: "Employee Status",
                    type: "dropdown",
                    data: [
                        {
                            key: "Permanent",
                            value: "Permanent"
                        },
                        {
                            key: "Contract",
                            value: "Contract"
                        }
                    ]
                },
                {
                    id: "Gender",
                    displayText: "Gender",
                    type: "dropdown",
                    data: [
                        {
                            key: "male",
                            value: "Male"
                        },
                        {
                            key: "female",
                            value: "Female"
                        }
                    ]
                },
                {
                    id: "PaymentMode",
                    displayText: "Payment Mode",
                    type: "dropdown",
                    data: [
                        {
                            key: "0",
                            value: "Cash"
                        },
                        {
                            key: "1",
                            value: "Bank"
                        }
                    ]
                },
                {
                    id: "MaritalStatus",
                    displayText: "Marital Status"
                },
                {
                    id: "Religion",
                    displayText: "Religion"
                },
                {
                    id: "BankAccountNo",
                    displayText: "Bank Account#"
                },
                {
                    id: "Address",
                    displayText: "Address"
                },
                {
                    id: "City",
                    displayText: "City"
                },
                {
                    id: "Country",
                    displayText: "Country"
                },
                {
                    id: "Domicile",
                    displayText: "Domicile"
                },
                {
                    id: "SocialSecurityNo",
                    displayText: "Social Security#"
                },
                {
                    id: "BloodGroup",
                    displayText: "Blood Group"
                },
                {
                    id: "Qualification",
                    displayText: "Qualification"
                },
                {
                    id: "EOBINo",
                    displayText: "EOBI#"
                },
                {
                    id: "DateOfBirth",
                    displayText: "Date Of Birth",
                    type: "date"
                },
                {
                    id: "Active",
                    displayText: "Active",
                    type: "checkbox"
                }
            ],
            defaultTerm: null,
            inputArray: null,
            savedFilters: this.savedFilters,
            showFilterSaving: null,
            simpleFieldLabel: null
        };

        this.advanceFilters = [
            {
                controlType: "text",
                key: "name",
                name: "name",
                label: "Employee Name",
                value: ""
            },
            {
                controlType: "text",
                key: "name",
                name: "name",
                label: "Employee Code",
                value: ""
            },
            {
                controlType: "text",
                key: "name",
                name: "name",
                label: "Punch Code",
                value: ""
            },
            {
                controlType: "dropdown",
                key: "name",
                name: "name",
                label: "Employee Status",
                value: "",
                data: [
                    {
                        key: "Permanent",
                        value: "Permanent"
                    },
                    {
                        key: "Contract",
                        value: "Contract"
                    }
                ]
            }
        ];
    }
    initcsvColumns() {
        var emp: Employee = this.employees.data[0];
        this.csvMapping = [];
        var keys = Object.keys(emp);
        for (var key in keys) {
            var obj = {
                csvColumn: keys[key],
                gridColumn: keys[key],
                defaultValue: null
            };
            if (obj.csvColumn == "organizationId") {
                obj.defaultValue = this.authService.getOrganizationId;
            }
            if (obj.csvColumn == "branchId") {
                obj.defaultValue = this.authService.getBranchId;
            }
            this.csvMapping.push(obj);
        }
    }

    getEmployees(): void {
        if (!this.customerParams.advanceFilters || this.customerParams.advanceFilters.length == 0) {
            var activeFilter = {
                fieldName: "Active",
                action: "=",
                searchTerm: this.activeEmployee,
                fieldType: "checkbox",
                id: 0
            };
            this.customerParams.advanceFilters = [activeFilter];
            this.customerParams.advancedSearchType = "And";
        }

        this.employeeService.advanceSearch(this.customerParams).subscribe((result) => {
            this.employees = result;
            this.employees.data.forEach((emp) => {
                emp.departmentName = "";
                var dpt = this.departments.find((x) => x.id == emp.departmentId);
                if (dpt) {
                    emp.departmentName = dpt.name;
                }
            });
        });
    }

    initColumns(): void {
        this.employeeColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "Full Name", dataKey: "fullName", isSortable: true, isShowable: true },
            { name: "Employee Code", dataKey: "employeeCode", isSortable: true, isShowable: true },
            { name: "Punch Code", dataKey: "punchCode", isSortable: true, isShowable: true },
            { name: "Department", dataKey: "departmentName", isSortable: false, isShowable: true },
            { name: "Employee Status", dataKey: "employeeStatus", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right" }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.customerParams.pageNumber = event.pageNumber;
        this.customerParams.pageSize = event.pageSize;
        this.getEmployees();
    }

    openForm(customer?: Employee): void {
        const dialogRef = this.dialog.open(EmployeeFormComponent, {
            data: customer
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getEmployees();
            }
        });
    }

    remove($event: string): void {
        this.employeeService.deleteEmployee($event).subscribe(() => {
            this.getEmployees();
            this.toastr.info("Employee Removed");
        });
    }

    sort($event: Sort): void {
        this.customerParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.customerParams.orderBy);
        this.getEmployees();
    }

    filter($event: string): void {
        this.customerParams.searchString = $event.trim().toLocaleLowerCase();
        this.customerParams.pageNumber = 0;
        this.customerParams.pageSize = 0;
        this.getEmployees();
    }

    reload(): void {
        this.customerParams.searchString = "";
        this.customerParams.pageNumber = 0;
        this.customerParams.pageSize = 0;
        this.getEmployees();
    }

    handleFileSelect(evt) {
        var files = evt.target.files;
        this.csvParser
            .parseXlsx(files[0], {
                header: true,
                delimiter: ",",
                mapping: this.csvMapping
            })
            .pipe()
            .subscribe(
                (result: Array<Employee>) => {
                    var valueArr = result.map(function (item) {
                        return item.employeeCode;
                    });
                    var isDuplicate = valueArr.some(function (item, idx) {
                        return valueArr.indexOf(item) != idx;
                    });
                    if (isDuplicate) {
                        this.toastr.error("Employee No is duplicate.");
                    }
                    isDuplicate = false;
                    var punchCodeArr = result.map(function (item) {
                        return item.punchCode;
                    });
                    isDuplicate = punchCodeArr.some(function (item, idx) {
                        return punchCodeArr.indexOf(item) != idx;
                    });

                    if (isDuplicate) {
                        this.toastr.error("Punch Code is duplicate.");
                    }

                    if (!isDuplicate) {
                        var model = {
                            employees: result
                        };
                        this.employeeService.importEmployee(model).subscribe(
                            (response) => {
                                response.messages.forEach((element) => {
                                    this.toastr.success(element);
                                });
                                this.reload();
                            },
                            (error) => {
                                var keys = Object.keys(error.errors);
                                console.log(keys);
                                for (var key in keys) {
                                    this.toastr.error(error.errors[keys[key]][0], "Import Employee", {
                                        timeOut: 0,
                                        disableTimeOut: true
                                    });
                                }
                            }
                        );
                    }
                    this.fileInput.nativeElement.value = "";
                },
                (error: NgxCSVParserError) => {
                    console.log("Error", error);
                }
            );
    }
    onImportFile($event) {
        this.initcsvColumns();
        this.fileInput.nativeElement.click();
    }
    onExportFile($event) {
        console.log($event);
        // this.csvParser.exportXls(this.employees.data, "Employees.xlsx", "Employees");
    }
    onAdvanceFilters($event) {
        if ($event == null) {
            this.customerParams.advanceFilters = [];
            this.customerParams.advancedSearchType = "";
        } else {
            if ($event.advancedTerms.length >= 0) {
                var exist = $event.advancedTerms.find((e) => e.fieldName === "Active");
                if (!exist) {
                    var activeFilter = {
                        fieldName: "Active",
                        action: "=",
                        searchTerm: this.activeEmployee,
                        fieldType: "checkbox",
                        id: 0
                    };
                    $event.advancedTerms.push(activeFilter);
                }
            }
            this.customerParams.advanceFilters = $event.advancedTerms;
            this.customerParams.advancedSearchType = $event.advancedSearchType;
            this.customerParams.pageNumber = 0;
            this.customerParams.pageSize = 10;
        }
        this.getEmployees();
    }
}

import { ContentObserver } from "@angular/cdk/observers";
import { Component, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { SearchParams } from "src/app/core/models/Filters/SearchParams";
import { Employee } from "../../../models/employee";
import { EmployeeService } from "../../../services/employee.service";

@Component({
    selector: "app-assign-department",
    templateUrl: "./assign-department.component.html",
    styleUrls: ["./assign-department.component.scss"]
})
export class AssignDepartmentComponent implements OnInit {
    departments: any[];
    employees: Employee[];
    title = "Assign Department";
    subtitle = "Bulk Assignment of Department To Employees";
    myControl = new FormControl();
    selectedUsers: Employee[] = new Array<Employee>();
    lastFilter: string = "";

    filteredOptions: Observable<Employee[]>;

    model: any = {
        departmentId: "",
        employeeIds: []
    };
    constructor(public employeeService: EmployeeService, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.loadLookups();
        this.filteredOptions = this.myControl.valueChanges.pipe(
            startWith<string | Employee[]>(""),
            map((value) => (typeof value === "string" ? value : this.lastFilter)),
            map((filter) => this._filter(filter))
        );
    }

    loadLookups() {
        let parms = new SearchParams();
        parms.pageSize = 1000;
        this.employeeService.getDepartmentLookup(parms).subscribe((res) => {
            this.departments = res.data;
        });
        this.employeeService.getEmployeesLookup(parms).subscribe((res) => {
            this.employees = res.data;
        });
    }

    private _filter(filter: string): Employee[] {
        this.lastFilter = filter;
        if (!this.employees) return [];
        if (filter) {
            return this.employees.filter((option) => {
                return option.fullName.toLowerCase().indexOf(filter.toLowerCase()) >= 0 || option.employeeCode.toLowerCase().indexOf(filter.toLowerCase()) >= 0;
            });
        } else {
            return this.employees.slice();
        }
    }

    displayFn(value: Employee[] | string): string | undefined {
        let displayValue: string;
        if (Array.isArray(value)) {
            value.forEach((user, index) => {
                if (index === 0) {
                    displayValue = user.fullName;
                } else {
                    displayValue += ", " + user.fullName;
                }
            });
        } else {
            displayValue = value;
        }
        return displayValue;
    }
    optionClicked(event: Event, user: Employee) {
        event.stopPropagation();
        this.toggleSelection(user);
    }
    toggleSelection(user: Employee) {
        user.selected = !user.selected;
        if (user.selected) {
            this.selectedUsers.push(user);
        } else {
            const i = this.selectedUsers.findIndex((value) => value.id === user.id);
            this.selectedUsers.splice(i, 1);
        }

        this.myControl.setValue(this.selectedUsers);
    }
    saveAssignDepartment() {
        console.log(this.model);
        if (!this.model.departmentId) {
            this.toastr.error("Please Select Department");
            return;
        }
        if (this.model.employeeIds.length == 0) {
            this.toastr.error("Please Select Employee");
            return;
        }
        this.employeeService.assignDepartment(this.model).subscribe((res) => {
            if (res.succeeded) {
                this.model = {
                    departmentId: "",
                    employeeIds: []
                };
                this.toastr.success(res.messages[0]);
            } else {
                this.toastr.error(res.messages[0]);
            }
            console.log(res);
        });
    }

    getEmployeeName(employeeId) {
        var employee = this.employees.find((x) => x.id == employeeId);
        if (employee) {
            return employee.fullName;
        }

        return "";
    }
}

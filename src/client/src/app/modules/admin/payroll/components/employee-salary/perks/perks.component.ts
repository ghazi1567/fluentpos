import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from "@angular/material-moment-adapter";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MatDatepicker } from "@angular/material/datepicker";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import * as moment from "moment";
import { Moment } from "moment";
import { ToastrService } from "ngx-toastr";
import { SalaryPerksTypeMapping, SalaryPerksType } from "src/app/core/enums/SalaryPerksType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { SalaryPerks } from "../../../models/salaryperks";
import { SearchParams } from "../../../models/SearchParams";
import { SalaryPerksService } from "../../../services/salary-perks.service";
export const Month_FORMATS = {
    parse: {
        dateInput: "MM/YYYY"
    },
    display: {
        dateInput: "MM/YYYY",
        monthYearLabel: "MMM YYYY",
        dateA11yLabel: "LL",
        monthYearA11yLabel: "MMMM YYYY"
    }
};

@Component({
    selector: "app-perks",
    templateUrl: "./perks.component.html",
    styleUrls: ["./perks.component.scss"],
    providers: [
        // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
        // application's root module. We provide it at the component level here, due to limitations of
        // our example generation script.
        {
            provide: DateAdapter,
            useClass: MomentDateAdapter,
            deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
        },
        { provide: MAT_DATE_FORMATS, useValue: Month_FORMATS }
    ]
})
export class PerksComponent implements OnInit {
    formGroup: FormGroup;
    formTitle: string;
    employeesLookup: Employee[];
    public minDate = new Date();
    perkData: SalaryPerks;
    public SalaryPerksTypeMapping = SalaryPerksTypeMapping;
    perksColumns: TableColumn[];
    salariesPerks: PaginatedResult<SalaryPerks>;
    salariesPerksParams = new SearchParams();

    constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialog, private salaryPerksService: SalaryPerksService, private toastr: ToastrService, private fb: FormBuilder) {}
    setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>, dataKey: string) {
        const ctrlValue = this.f[dataKey].value;
        ctrlValue.month(normalizedMonthAndYear.month());
        ctrlValue.year(normalizedMonthAndYear.year());
        this.f[dataKey].setValue(ctrlValue);
        datepicker.close();
    }

    ngOnInit(): void {
        this.initializeForm();
        this.initColumns();
        this.getAllSalariesPerks();
    }

    initializeForm() {
        this.formGroup = this.fb.group({
            id: [this.perkData && this.perkData.id],
            employeeId: [this.data && this.data.event.employeeId, Validators.required],
            name: [this.perkData && this.perkData.name, Validators.required],
            type: [(this.perkData && this.perkData.type) || this.data.type, Validators.required],
            percentage: [(this.perkData && this.perkData.percentage) || 0],
            amount: [this.perkData && this.perkData.amount, Validators.required],
            isRecursion: [(this.perkData && this.perkData.isRecursion) || false],
            isRecursionUnLimited: [(this.perkData && this.perkData.isRecursionUnLimited) || true],
            recursionEndMonth: [(this.perkData && this.perkData.recursionEndMonth) || moment()],
            description: [this.perkData && this.perkData.description],
            isTaxable: [(this.perkData && this.perkData.isTaxable) || false],
            effecitveFrom: [(this.perkData && this.perkData.effecitveFrom) || moment()]
        });
        if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
            this.formTitle = "Register " + SalaryPerksTypeMapping[this.data.type];
        } else {
            this.formTitle = "Edit " + SalaryPerksTypeMapping[this.data.type];
        }
    }
    get f() {
        return this.formGroup.controls;
    }
    getAllSalariesPerks() {
        this.salariesPerksParams.employeeId = this.data.event.employeeId;
        this.salariesPerksParams.type = this.data.type;
        this.salaryPerksService.getAll(this.salariesPerksParams).subscribe((res) => {
            this.salariesPerks = res;
        });
    }

    initColumns(): void {
        this.perksColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: false },
            { name: "Name", dataKey: "name", isSortable: true, isShowable: true },
            { name: "Amount", dataKey: "amount", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons: ["Update", "Remove"] }
        ];
    }
    pageChanged(event: PaginatedFilter): void {
        this.salariesPerksParams.pageNumber = event.pageNumber;
        this.salariesPerksParams.pageSize = event.pageSize;
        this.getAllSalariesPerks();
    }
    openForm(perk?: SalaryPerks): void {
        console.log(perk);
        this.perkData = perk;
        this.formGroup.patchValue(this.perkData);
        this.isRecursion = this.perkData.isRecursion;
        if (this.perkData.isRecursion && !this.perkData.isRecursionUnLimited) {
            this.isLimited = true;
        }
    }
    reload(): void {
        this.salariesPerksParams.searchString = "";
        this.salariesPerksParams.pageNumber = 0;
        this.salariesPerksParams.pageSize = 0;
        this.getAllSalariesPerks();
    }
    onPercentageChange($event) {
        console.log($event.target.value);

        var amount = (($event.target.value / 100) * this.data.event.currentSalary).toFixed(2);
        var expectedSalary = this.data.event.currentSalary;

        if (this.data.type == SalaryPerksType.Increment) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) + parseFloat(amount)).toFixed(2);
        } else if (this.data.type == SalaryPerksType.Decrement) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) - parseFloat(amount)).toFixed(2);
        }
        this.formGroup.patchValue({
            amount: amount,
            expectedSalary: expectedSalary
        });
    }

    onAmountChange($event) {
        console.log($event.target.value);
        var percentage = (($event.target.value / this.data.event.currentSalary) * 100).toFixed(2);
        var expectedSalary = this.data.event.currentSalary;

        if (this.data.type == SalaryPerksType.Increment) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) + parseFloat($event.target.value)).toFixed(2);
        } else if (this.data.type == SalaryPerksType.Decrement) {
            expectedSalary = (parseFloat(this.data.event.currentSalary) - parseFloat($event.target.value)).toFixed(2);
        }

        this.formGroup.patchValue({
            percentage: percentage,
            expectedSalary: expectedSalary
        });
    }

    onSubmit() {
        if (this.formGroup.valid) {
            if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
                this.salaryPerksService.create(this.formGroup.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.salaryPerksService.update(this.formGroup.value).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            }
        }
    }
    isRecursion = false;
    isLimited = false;
    recursionChange($event) {
        console.log($event);
        this.isRecursion = $event.value;
        this.f.isRecursionUnLimited.setValue(this.isRecursion);
        this.isLimited = false;
    }
    recursionLimitChange($event) {
        console.log($event);
        this.isLimited = !$event.value;
    }
    remove($event: string): void {
        this.salaryPerksService.delete($event).subscribe(() => {
            this.getAllSalariesPerks();
            this.toastr.info("Perks Removed");
        });
    }
}

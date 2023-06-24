import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormGroup, UntypedFormBuilder, Validators } from "@angular/forms";
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from "@angular/material-moment-adapter";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MatDatepicker } from "@angular/material/datepicker";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import * as moment from "moment";
import { Moment } from "moment";
import { ToastrService } from "ngx-toastr";
import { SalaryPerksTypeMapping, SalaryPerksType, GlobalPerkType, GlobalPerkTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Employee } from "src/app/modules/admin/people/models/employee";
import { SalaryPerks } from "../../../../models/salaryperks";
import { SearchParams } from "../../../../models/SearchParams";
import { SalaryPerksService } from "../../../../services/salary-perks.service";

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
    selector: "app-global-perk-form",
    templateUrl: "./global-perk-form.component.html",
    styleUrls: ["./global-perk-form.component.scss"]
})
export class GlobalPerkFormComponent implements OnInit {
    formGroup: UntypedFormGroup;
    formTitle: string;
    employeesLookup: Employee[];
    public minDate = new Date();
    perkData: SalaryPerks;
    public SalaryPerksTypeMapping = SalaryPerksTypeMapping;
    public salaryPerksType = Object.values(SalaryPerksType).filter((value) => typeof value === "number");

    globalPerkTypeData: GlobalPerkType;
    public GlobalPerkTypeMapping = GlobalPerkTypeMapping;
    public globalPerkTypes = Object.values(GlobalPerkType).filter((value) => typeof value === "number");
    perksColumns: TableColumn[];
    salariesPerks: PaginatedResult<SalaryPerks>;
    salariesPerksParams = new SearchParams();

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private dialogRef: MatDialog,
        private salaryPerksService: SalaryPerksService,
        private toastr: ToastrService,
        private fb: UntypedFormBuilder
    ) {}
    setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>, dataKey: string) {
        const ctrlValue = this.f[dataKey].value;
        ctrlValue.month(normalizedMonthAndYear.month());
        ctrlValue.year(normalizedMonthAndYear.year());
        this.f[dataKey].setValue(ctrlValue);
        datepicker.close();
    }

    ngOnInit(): void {
        this.initializeForm();
    }

    initializeForm() {
        this.formGroup = this.fb.group({
            id: [this.perkData && this.perkData.id],
            name: [this.perkData && this.perkData.name, Validators.required],
            type: [(this.perkData && this.perkData.type) || (this.data && this.data.type), Validators.required],
            globalPerkType: [this.perkData && this.perkData.globalPerkType, Validators.required],
            percentage: [(this.perkData && this.perkData.percentage) || 0],
            amount: [this.perkData && this.perkData.amount, Validators.required],
            isRecursion: [(this.perkData && this.perkData.isRecursion) || false],
            isRecursionUnLimited: [(this.perkData && this.perkData.isRecursionUnLimited) || true],
            recursionEndMonth: [(this.perkData && this.perkData.recursionEndMonth) || moment()],
            description: [this.perkData && this.perkData.description],
            isTaxable: [(this.perkData && this.perkData.isTaxable) || false],
            effecitveFrom: [(this.perkData && this.perkData.effecitveFrom) || moment()],
            isGlobal: [true]
        });
        this.formTitle = "Define New Incentive/Deduction";
        // if (this.formGroup.get("id").value === "" || this.formGroup.get("id").value == null) {
        //
        // } else {
        //     this.formTitle = "Edit " + SalaryPerksTypeMapping[this.data && this.data.type];
        // }
    }
    get f() {
        return this.formGroup.controls;
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
        this.isRecursion = $event.value;
        this.f.isRecursionUnLimited.setValue(this.isRecursion);
        this.isLimited = false;
    }
    recursionLimitChange($event) {
        this.isLimited = !$event.value;
    }
}

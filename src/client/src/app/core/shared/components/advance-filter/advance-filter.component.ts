import { Component, Inject, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { AdvanceFilter } from "src/app/core/models/Filters/AdvanceFilter";

@Component({
    selector: "app-advance-filter",
    templateUrl: "./advance-filter.component.html",
    styleUrls: ["./advance-filter.component.scss"]
})
export class AdvanceFilterComponent implements OnInit {
    filters: AdvanceFilter[];
    formTitle: string;
    public formGroup: FormGroup = this.fb.group({});
    constructor(public dialogRef: MatDialogRef<AdvanceFilterComponent>, @Inject(MAT_DIALOG_DATA) public _data: AdvanceFilter[], private fb: FormBuilder) {
        this.filters =_data;
    }

    ngOnInit(): void {
        this.formTitle ='Advance Filters'
        this.initializeForm();
        console.log(this.filters);
    }

    initializeForm() {
        for (const control of this.filters) {
            this.formGroup.addControl(control.name, this.fb.control(control.value, []));
        }
    }
    onSubmit() {
        console.log(this.formGroup.value);
    }
}

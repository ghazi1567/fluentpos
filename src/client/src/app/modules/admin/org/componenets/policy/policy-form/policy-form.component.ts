import { Component, Inject, OnInit } from "@angular/core";
import { Validators, FormBuilder, FormGroup } from "@angular/forms";
import { MatDialog, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { EarlyArrivalPolicy, EarlyArrivalPolicyMapping } from "src/app/core/enums/EarlyArrivalPolicy";
import { EarnedHourPolicy, EarnedHourPolicyMapping } from "src/app/core/enums/EarnedHourPolicy";
import { LateComersPenaltyType, LateComersPenaltyTypeMapping } from "src/app/core/enums/LateComersPenaltyType";
import { OverTime, OverTimeMapping } from "src/app/core/enums/OverTime";
import { PayPeriod, PayPeriodMapping } from "src/app/core/enums/PayPeriod";
import { PayslipType, PayslipTypeMapping } from "src/app/core/enums/PayslipType";
import { TimeoutPolicy, TimeoutPolicyMapping } from "src/app/core/enums/TimeoutPolicy";
import { Policy } from "../../../models/policy";
import { PolicyService } from "../../../services/policy.service";

@Component({
    selector: "app-policy-form",
    templateUrl: "./policy-form.component.html",
    styleUrls: ["./policy-form.component.scss"]
})
export class PolicyFormComponent implements OnInit {
    firstFormGroup: FormGroup;
    secondFormGroup: FormGroup;
    thirdFormGroup: FormGroup;
    forthFormGroup: FormGroup;
    formTitle: string;
    public PayslipTypeMapping = PayslipTypeMapping;
    public payslipTypes = Object.values(PayslipType).filter((value) => typeof value === "number");

    public PayPeriodMapping = PayPeriodMapping;
    public payPeriods = Object.values(PayPeriod).filter((value) => typeof value === "number");

    public EarlyArrivalPolicyMapping = EarlyArrivalPolicyMapping;
    public earlyArrivalPolicy = Object.values(EarlyArrivalPolicy).filter((value) => typeof value === "number");

    public TimeoutPolicyMapping = TimeoutPolicyMapping;
    public timeoutPolicy = Object.values(TimeoutPolicy).filter((value) => typeof value === "number");

    public LateComersPenaltyTypeMapping = LateComersPenaltyTypeMapping;
    public lateComersPenaltyTypes = Object.values(LateComersPenaltyType).filter((value) => typeof value === "number");

    public OverTimeMapping = OverTimeMapping;
    public overTimes = Object.values(OverTime).filter((value) => typeof value === "number");

    public EarnedHourPolicyMapping = EarnedHourPolicyMapping;
    public earnedHourPolicy = Object.values(EarnedHourPolicy).filter((value) => typeof value === "number");

    constructor(@Inject(MAT_DIALOG_DATA) public data: Policy, private dialogRef: MatDialog, private fb: FormBuilder, public policyService: PolicyService, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.initializeForm();
        console.log(this.PayslipTypeMapping);
    }

    initializeForm() {
        this.firstFormGroup = this.fb.group({
            id: [this.data && this.data.id],
            name: [this.data && this.data.name, Validators.required],
            departmentId: [this.data && this.data.departmentId]
        });
        this.secondFormGroup = this.fb.group({
            payslipType: [this.data && this.data.payslipType, Validators.required],
            payPeriod: [this.data && this.data.payPeriod, Validators.required],
            allowedOffDays: [(this.data && this.data.allowedOffDays) || 0]
        });
        this.thirdFormGroup = this.fb.group({
            shiftStartTime: [this.data && this.data.shiftStartTime, Validators.required],
            shiftEndTime: [this.data && this.data.shiftEndTime, Validators.required],
            allowedLateMinutes: [(this.data && this.data.allowedLateMinutes) || 0],
            allowedLateMinInMonth: [(this.data && this.data.allowedLateMinInMonth) || 0],
            forceTimeout: [(this.data && this.data.forceTimeout) || 0],
            earlyArrivalPolicy: [this.data && this.data.earlyArrivalPolicy, Validators.required],
            timeoutPolicy: [this.data && this.data.timeoutPolicy, Validators.required],
            isMonday: [(this.data && this.data.isMonday) || false],
            isTuesday: [(this.data && this.data.isTuesday) || false],
            isWednesday: [(this.data && this.data.isWednesday) || false],
            isThursday: [(this.data && this.data.isThursday) || false],
            isFriday: [(this.data && this.data.isFriday) || false],
            isSaturday: [(this.data && this.data.isSaturday) || false],
            isSunday: [(this.data && this.data.isSunday) || false],
            lateComersPenalty: [(this.data && this.data.lateComersPenalty) || 0],
            lateComersPenaltyType: [this.data && this.data.lateComersPenaltyType, Validators.required],
            earnedHourPolicy: [this.data && this.data.earnedHourPolicy, Validators.required],
            sandwichLeaveCount: [this.data && this.data.sandwichLeaveCount || 0],
            dailyWorkingHour: [this.data && this.data.dailyWorkingHour || 0],
        });
        this.forthFormGroup = this.fb.group({
            dailyOverTime: [this.data && this.data.dailyOverTime, Validators.required],
            holidayOverTime: [this.data && this.data.holidayOverTime, Validators.required],
            dailyOverTimeRate: [(this.data && this.data.dailyOverTimeRate) || 0],
            holidayOverTimeRate: [(this.data && this.data.holidayOverTimeRate) || 0]
        });
        if (this.firstFormGroup.get("id").value === "" || this.firstFormGroup.get("id").value == null) {
            this.formTitle = "Register Policy";
        } else {
            this.formTitle = "Edit Policy";
        }
    }

    onSubmit() {
        if (this.firstFormGroup.valid && this.secondFormGroup.valid && this.thirdFormGroup.valid && this.forthFormGroup.valid) {
            var formData = { ...this.firstFormGroup.value, ...this.secondFormGroup.value, ...this.thirdFormGroup.value, ...this.forthFormGroup.value };

            if (this.firstFormGroup.get("id").value === "" || this.firstFormGroup.get("id").value == null) {
                this.policyService.create(formData).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.policyService.update(formData).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach(element => {
                            this.toastr.error(element);
                        });
                    }
                );
            }
        }
    }
}

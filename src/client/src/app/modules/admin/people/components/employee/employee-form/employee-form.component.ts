import { Component, Inject, OnInit } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { PaymentMode, PaymentModeMapping } from "src/app/core/enums/PaymentMode";
import { Policy } from "src/app/modules/admin/org/models/policy";
import { SearchParams } from "src/app/modules/admin/org/models/SearchParams";
import { PolicyService } from "src/app/modules/admin/org/services/policy.service";
import { Employee } from "../../../models/employee";
import { EmployeeService } from "../../../services/employee.service";

@Component({
    selector: "app-employee-form",
    templateUrl: "./employee-form.component.html",
    styleUrls: ["./employee-form.component.scss"]
})
export class EmployeeFormComponent implements OnInit {
    firstFormGroup: UntypedFormGroup;
    secondFormGroup: UntypedFormGroup;
    thirdFormGroup: UntypedFormGroup;
    forthFormGroup: UntypedFormGroup;
    formTitle: string;

    policies: any[];
    departments: any[];
    designations: any[];
    filteredDesignations: any[];
    employeesLookup: Employee[];
    public PaymentModeMapping = PaymentModeMapping;
    public paymentModes = Object.values(PaymentMode).filter((value) => typeof value === "number");

    constructor(@Inject(MAT_DIALOG_DATA) public data: Employee, private dialogRef: MatDialog, private fb: UntypedFormBuilder, public employeeService: EmployeeService, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.initializeForm();
        this.loadLookups();
    }

    loadLookups() {
        let parms = new SearchParams();
        this.employeeService.getPolicyLookup(parms).subscribe((res) => {
            this.policies = res.data;
        });
        this.employeeService.getDepartmentLookup(parms).subscribe((res) => {
            this.departments = res.data;
        });
        this.employeeService.getDesignationLookup(parms).subscribe((res) => {
            this.designations = res.data;
            this.filteredDesignations = this.designations.filter((x) => x.departmentId == this.data.departmentId);
        });
        this.employeeService.getEmployees(parms).subscribe((res) => {
            this.employeesLookup = res.data.filter((x) => x.id != this.data.id);
        });
    }

    initializeForm() {
        this.firstFormGroup = this.fb.group({
            id: [this.data && this.data.id],
            employeeCode: [this.data && this.data.employeeCode, Validators.required],
            punchCode: [this.data && this.data.punchCode, Validators.required],
            firstName: [this.data && this.data.firstName, Validators.required],
            lastName: [this.data && this.data.lastName, Validators.required],
            departmentId: [this.data && this.data.departmentId, Validators.required],
            designationId: [this.data && this.data.designationId, Validators.required],
            policyId: [this.data && this.data.policyId, Validators.required],
            mobileNo: [this.data && this.data.mobileNo],
            basicSalary: [this.data && this.data.basicSalary],
            reportingTo: [this.data && this.data.reportingTo],
            employeeStatus: [(this.data && this.data.employeeStatus) || "Permanent"],
            gender: [this.data && this.data.gender, Validators.required]
        });
        this.secondFormGroup = this.fb.group({
            fatherName: [this.data && this.data.fatherName],
            motherName: [this.data && this.data.motherName],
            phoneNo: [this.data && this.data.phoneNo],
            address: [this.data && this.data.address],
            allowManualAttendance: [(this.data && this.data.allowManualAttendance) || true],
            dateOfBirth: [this.data && this.data.dateOfBirth],
            placeOfBirth: [this.data && this.data.placeOfBirth],
            cnicNo: [this.data && this.data.cnicNo],
            cnicIssueDate: [this.data && this.data.cnicIssueDate],
            cnicExpireDate: [this.data && this.data.cnicExpireDate],
            joiningDate: [this.data && this.data.joiningDate],
            email: [this.data && this.data.email],
            city: [this.data && this.data.city],
            country: [this.data && this.data.country],
            nicPlace: [this.data && this.data.nicPlace],
            domicile: [this.data && this.data.domicile]
        });
        this.thirdFormGroup = this.fb.group({
            paymentMode: [this.data && this.data.paymentMode],
            bankAccountNo: [this.data && this.data.bankAccountNo],
            bankAccountTitle: [this.data && this.data.bankAccountTitle],
            bankBranchCode: [this.data && this.data.bankBranchCode],
            bankName: [this.data && this.data.bankName],
            eobiNo: [this.data && this.data.eobiNo],
            qualification: [this.data && this.data.qualification],
            bloodGroup: [this.data && this.data.bloodGroup],
            languages: [this.data && this.data.languages],
            socialSecurityNo: [this.data && this.data.socialSecurityNo],
            maritalStatus: [this.data && this.data.maritalStatus],
            religion: [this.data && this.data.religion],
        });
        if (this.firstFormGroup.get("id").value === "" || this.firstFormGroup.get("id").value == null) {
            this.formTitle = "Register Employee";
        } else {
            this.formTitle = "Edit Employee";
        }
    }

    departmentSelection(event) {
        this.filteredDesignations = this.designations.filter((x) => x.departmentId == event.value);
    }

    onSubmit() {
        if (this.firstFormGroup.valid) {
            var formData = { ...this.firstFormGroup.value, ...this.secondFormGroup?.value, ...this.thirdFormGroup?.value, ...this.forthFormGroup?.value };
            formData.fullName = `${formData.firstName} ${formData.lastName}`;

            if (formData.paymentMode == PaymentMode.Bank && (formData.bankAccountNo == null || formData.bankAccountNo == "")) {
                this.toastr.error("Please enter bank account no");
                return;
            }

            if (this.firstFormGroup.get("id").value === "" || this.firstFormGroup.get("id").value == null) {
                this.employeeService.createEmployee(formData).subscribe((response) => {
                    this.toastr.success(response.messages[0]);
                    this.dialogRef.closeAll();
                });
            } else {
                this.employeeService.updateEmployee(formData).subscribe(
                    (response) => {
                        this.toastr.success(response.messages[0]);
                        this.dialogRef.closeAll();
                    },
                    (error) => {
                        error.messages.forEach((element) => {
                            this.toastr.error(element);
                        });
                    }
                );
            }
        }
    }
}

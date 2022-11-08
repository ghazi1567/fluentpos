import { PayPeriod } from "src/app/core/enums/PayPeriod";

export interface PayrollRequest {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    startDate: Date;
    endDate: Date;
    month: number;
    payPeriod: PayPeriod;
    salaryCalculationFormula: any;
    ignoreAttendance: boolean;
    ignoreDeductionForAbsents: boolean;
    ignoreDeductionForLateComer: boolean;
    ignoreOvertime: boolean;
    monthName: string;
    payPeriodName: string;
    status: string;
    startedAt?: Date;
    endedAt?: Date;
    run: boolean;
}

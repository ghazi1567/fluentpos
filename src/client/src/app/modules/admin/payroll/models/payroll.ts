import { PayrollTransaction } from "./PayrollTransaction";

export interface Payroll {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    payrollRequestId: string;

    employeeId: string;

    startDate: Date;

    endDate: Date;

    employeeSalary: number;

    requiredDays: number;

    earnedDays: number;

    totalDays: number;

    presentDays: number;

    absentDays: number;

    leaves: number;

    allowedOffDays: number;

    paymentMode: number;

    totalEarned: number;

    totalIncentive: number;

    totalDeduction: number;

    totalOvertime: number;

    netPay: number;
    employeeName: string;
    transactions: PayrollTransaction[];
}

export interface Salary {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    employeeId: string;

    basicSalary: number;
    currentSalary: number;

    incentive: number;

    deduction: number;

    payableSalary: number;

    perDaySalary: number;

    perHourSalary: number;

    totalDaysInMonth: number;
    employeeName: string;
    active: boolean;
}

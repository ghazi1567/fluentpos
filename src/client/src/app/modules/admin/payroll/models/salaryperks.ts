import { SalaryPerksType } from "src/app/core/enums/SalaryPerksType";

export interface SalaryPerks {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    employeeId: string;
    type: SalaryPerksType;
    percentage: string;
    amount: string;
    isRecursion: string;
    isRecursionUnLimited: string;
    recursionEndMonth: string;
    description: string;
    effecitveFrom: string;
    isTaxable: string;
}

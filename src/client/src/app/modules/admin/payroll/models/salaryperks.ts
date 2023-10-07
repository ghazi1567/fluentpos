import { GlobalPerkType, SalaryPerksType } from "src/app/core/enums/SalaryPerksType";

export interface SalaryPerks {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    name: string;
    employeeId: string;
    type: SalaryPerksType;
    percentage: string;
    amount: string;
    isRecursion: boolean;
    isRecursionUnLimited: boolean;
    recursionEndMonth: string;
    description: string;
    effecitveFrom: string;
    isTaxable: string;
    isGlobal: string;
    globalPerkType: GlobalPerkType;
}

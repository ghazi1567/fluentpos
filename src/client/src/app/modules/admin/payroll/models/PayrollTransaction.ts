import { EntryType } from "src/app/core/enums/EntryType";
import { TransactionType } from "src/app/core/enums/TransactionType";

export interface PayrollTransaction {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    payrollRequestId: string;
    payrollId: string;

    transactionType: TransactionType;

    entryType: EntryType;

    transactionName: string;

    earning: number;

    deduction: number;

    daysOrHours: number;

    perDaySalary: number;
}

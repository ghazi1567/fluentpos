export enum SalaryPerksType {
    Increment = 1,
    Decrement = 2,
    Incentives = 3,
    Deductions = 4
}
export const SalaryPerksTypeMapping: Record<SalaryPerksType, string> = {
    [SalaryPerksType.Increment]: "Increment",
    [SalaryPerksType.Decrement]: "Decrement",
    [SalaryPerksType.Incentives]: "Incentives",
    [SalaryPerksType.Deductions]: "Deductions"
};

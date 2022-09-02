export enum OverTime {
    UnPaid = 1,
    EqualSalaryPerHour = 2,
    SalaryPerHourMultiplyX = 3,
    FixedRate = 4,
    EqualSalaryPerDay = 5
}
export const OverTimeMapping: Record<OverTime, string> = {
    [OverTime.UnPaid]: "UnPaid",
    [OverTime.EqualSalaryPerHour]: "Equal Salary/Hour",
    [OverTime.SalaryPerHourMultiplyX]: "Salary/Hour Multiply X",
    [OverTime.FixedRate]: "FixedRate",
    [OverTime.EqualSalaryPerDay]: "Equal Salary/Day"
};

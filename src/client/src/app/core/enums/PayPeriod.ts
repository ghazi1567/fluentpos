export enum PayPeriod {
    None = 0,
    // Daily = 1,
    // Weekly = 2,
    HalfMonth = 3,
    Monthly = 4
}
export const PayPeriodMapping: Record<PayPeriod, string> = {
    [PayPeriod.None]: "None",
    // [PayPeriod.Daily]: "Daily",
    // [PayPeriod.Weekly]: "Weekly",
    [PayPeriod.HalfMonth]: "15 Days",
    [PayPeriod.Monthly]: "Monthly"
};

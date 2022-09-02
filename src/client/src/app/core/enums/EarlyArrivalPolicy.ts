export enum EarlyArrivalPolicy {
    ActualTime = 1,
    ShiftTime = 2
}
export const EarlyArrivalPolicyMapping: Record<EarlyArrivalPolicy, string> = {
    [EarlyArrivalPolicy.ActualTime]: "Actual Time",
    [EarlyArrivalPolicy.ShiftTime]: "Shift Time",
};
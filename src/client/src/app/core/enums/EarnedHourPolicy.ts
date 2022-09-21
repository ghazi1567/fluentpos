export enum EarnedHourPolicy {
    ShiftHour = 1,
    ActualHour = 2
}
export const EarnedHourPolicyMapping: Record<EarnedHourPolicy, string> = {
    [EarnedHourPolicy.ShiftHour]: "Shift Hours only",
    [EarnedHourPolicy.ActualHour]: "Actual Hours"
};
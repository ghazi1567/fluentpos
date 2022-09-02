export enum TimeoutPolicy {
    Nothing = 0,
    Present = 1,
    Absent = 2,
    HalfDay = 3
}
export const TimeoutPolicyMapping: Record<TimeoutPolicy, string> = {
    [TimeoutPolicy.Nothing]: "Do Nothing",
    [TimeoutPolicy.Present]: "Present",
    [TimeoutPolicy.Absent]: "Absent",
    [TimeoutPolicy.HalfDay]: "Half Day"
};
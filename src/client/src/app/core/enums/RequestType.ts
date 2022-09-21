export enum RequestType {
    Attendance = 1,
    OverTime = 2
}
export const RequestTypeMapping: Record<RequestType, string> = {
    [RequestType.Attendance]: "Attendance",
    [RequestType.OverTime]: "OverTime"
};

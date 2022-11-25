export enum RequestType {
    Attendance = 1,
    OverTime = 2,
    AttendanceModify = 3,
    OverTimeModify = 4,
}
export const RequestTypeMapping: Record<RequestType, string> = {
    [RequestType.Attendance]: "Attendance",
    [RequestType.OverTime]: "OverTime",
    [RequestType.AttendanceModify]: "Attendance Modification",
    [RequestType.OverTimeModify]: "OverTime Modification",
};

export enum AttendanceStatus {
    None = 0,
    Present = 1,
    Absent = 2,
    Leave = 3,
    Holiday = 4,
    Off = 5
}
export const AttendanceStatusMapping: Record<AttendanceStatus, string> = {
    [AttendanceStatus.None]: "-",
    [AttendanceStatus.Present]: "Present",
    [AttendanceStatus.Absent]: "Absent",
    [AttendanceStatus.Leave]: "Leave",
    [AttendanceStatus.Holiday]: "Holiday",
    [AttendanceStatus.Off]: "Off Day"
};

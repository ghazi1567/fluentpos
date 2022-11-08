export enum AttendanceType {
    Bio = 0,
    Manual = 1,
    OverTime = 2,
    System = 3
}
export const AttendanceTypeMapping: Record<AttendanceType, string> = {
    [AttendanceType.Bio]: "Bio Matric",
    [AttendanceType.Manual]: "Manual",
    [AttendanceType.OverTime]: "Overtime",
    [AttendanceType.System]: "System Generated"
};

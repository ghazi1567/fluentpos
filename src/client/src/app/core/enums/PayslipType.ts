export enum PayslipType {
    AttendanceBase = 1,
    TimeBase = 2,
    HourlyBase = 3
}
export const PayslipTypeMapping: Record<PayslipType, string> = {
    [PayslipType.AttendanceBase]: "Attendance Base",
    [PayslipType.TimeBase]: "Time Base",
    [PayslipType.HourlyBase]: "Hourly Base"
};

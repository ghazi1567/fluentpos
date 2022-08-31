namespace FluentPOS.Shared.DTOs.Enums
{
    /// <summary>
    /// ATTENDENCE BASED:EMPLOYEES HAVE TO MARK THEIR ATTENDANCE IN A DAY DESPITE COMPLETING THEIR REQUIRED HOURS.
    /// TIME BASED: EMPLOYEE HAVE TO COMPLETE THEIR ASSIGNED HOURS WITHIN SHIFT-TIME. EG: 9:00 Am TO 5:00 Pm
    /// HOUR BASED:EMPLOYEES HAVE TO COMPLETE THEIR REQUIRED HOURS ANYTIME WITHIN 24HRS.
    /// </summary>
    public enum PayslipType
    {
        /// <summary>
        /// EMPLOYEES HAVE TO MARK THEIR ATTENDANCE IN A DAY DESPITE COMPLETING THEIR REQUIRED HOURS.
        /// </summary>
        AttendanceBase = 1,

        /// <summary>
        /// EMPLOYEE HAVE TO COMPLETE THEIR ASSIGNED HOURS WITHIN SHIFT-TIME.
        /// </summary>
        TimeBase = 2,

        /// <summary>
        /// EMPLOYEES HAVE TO COMPLETE THEIR REQUIRED HOURS ANYTIME WITHIN 24HRS.
        /// </summary>
        HourlyBase = 3
    }
}
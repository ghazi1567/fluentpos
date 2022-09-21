using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Dtos.Organizations
{
    public class PolicyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid BranchId { get; set; }

        public Guid? DepartmentId { get; set; }

        public PayslipType PayslipType { get; set; }

        /// <summary>
        /// SALARY WILL BE GENERATED IN THIS PERIOD.
        /// EG: Daily, Weekly, MidMonth, Monthly.
        /// </summary>
        public PayPeriod PayPeriod { get; set; }

        /// <summary>
        /// MAXIMUM NUMBER OF OFF DAYS OTHER THAN LEAVES THAT AN EMPLOYEE CAN AVAIL IN A MONTH.
        /// </summary>
        public int AllowedOffDays { get; set; }

        /// <summary>
        /// only for hourly payslip type.
        /// </summary>
        public int? RequiredWorkingHour { get; set; }

        public TimeSpan ShiftStartTime { get; set; }

        public TimeSpan ShiftEndTime { get; set; }

        /// <summary>
        /// GRACE PERIOD. ENTER THE NUMBER OF MINUTES THE SYSTEM SHOULD WAIT BEFORE MARKING THE EMPLOYEE LATE.
        /// </summary>
        public int AllowedLateMinutes { get; set; }

        /// <summary>
        /// ALLOCATED TO AN EMPLOYEE FOR A MONTH
        /// </summary>
        public int AllowedLateMinInMonth { get; set; }

        /// <summary>
        /// ACTUAL TIME: THE SYSTEM CALCULATES THE WORKING HOURS OF AN EMPLOYEE AS SOON AS HE/SHE MARKS THEIR ATTENDANCE.
        /// SHIFT TIME: THE SYSTEM CALCULATES THE WORKING HOURS OF AN EMPLOYEE AFTER HIS/HER SHIFT STARTS.
        /// </summary>
        public EarlyArrivalPolicy EarlyArrivalPolicy { get; set; }

        /// <summary>
        /// SPECIFY THE NUMBER OF HOURS THE SYSTEM SHOULD WAIT ­­­FOR AN EMPLOYEE TO MARK HIS SIGN OUT AFTER CLOSING. THE SYSTEM WILL AUTOMATICALLY SIGNOUT THE EMPLOYEE IF THE FORCE TIME OUT ASSIGNED HOURS HAVE SURPASSED.
        /// </summary>
        public int ForceTimeout { get; set; }

        public TimeoutPolicy TimeoutPolicy { get; set; }

        public bool IsMonday { get; set; }

        public bool IsTuesday { get; set; }

        public bool IsWednesday { get; set; }

        public bool IsThursday { get; set; }

        public bool IsFriday { get; set; }

        public bool IsSaturday { get; set; }

        public bool IsSunday { get; set; }

        public OverTime DailyOverTime { get; set; }

        public OverTime HolidayOverTime { get; set; }

        public LateComersPenaltyType lateComersPenaltyType { get; set; }

        public int lateComersPenalty { get; set; }

        public int DailyOverTimeRate { get; set; }

        public int HolidayOverTimeRate { get; set; }

        public EarnedHourPolicy EarnedHourPolicy { get; set; }
    }
}
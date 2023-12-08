using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Attendance : BaseEntity
    {
        public long EmployeeId { get; set; }

        public long DepartmentId { get; set; }

        public long PolicyId { get; set; }

        public long DesignationId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Bio Or Request.
        /// </summary>
        public AttendanceType AttendanceType { get; set; }

        public long? RequestId { get; set; }

        public DateTime ActualIn { get; set; }

        public DateTime ActualOut { get; set; }

        public DateTime ExpectedIn { get; set; }

        public DateTime ExpectedOut { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public long ApprovedBy { get; set; }

        public string Reason { get; set; }

        public double EarnedHours { get; set; }

        public double EarnedMinutes { get; set; }

        public double OvertimeHours { get; set; }

        public double OvertimeMinutes { get; set; }

        public string BioMachineId { get; set; }

        public double DeductedHours { get; set; }

        public double LateMinutes { get; set; }

        /// <summary>
        /// Only contains shift hours or overtime if seperate entry
        /// </summary>
        public double TotalEarnedHours { get; set; }

        public double ActualEarnedHours { get; set; }

        public bool IsLateComer { get; set; }

        public bool IsCheckOutMissing { get; set; }

        public OverTimeType OverTimeType { get; set; }

        public double Production { get; set; }

        public double RequiredProduction { get; set; }

        public bool IsNextDay { get; set; }

        public string PunchCode { get; set; }

        public string CardNo { get; set; }

        public ShiftType ShiftType { get; set; }

        public long? ShiftId { get; set; }

        public long? OvertimeRequestId { get; set; }
    }
}
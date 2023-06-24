using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Dtos.Peoples
{
    public class AttendanceDto : BaseEntityDto
    {
        public Guid EmployeeId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid PolicyId { get; set; }

        public Guid DesignationId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Bio Or Request.
        /// </summary>
        public AttendanceType AttendanceType { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        public Guid? RequestId { get; set; }

        public DateTime ActualIn { get; set; }

        public DateTime ActualOut { get; set; }

        public DateTime ExpectedIn { get; set; }

        public DateTime ExpectedOut { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public Guid ApprovedBy { get; set; }

        public string Reason { get; set; }

        public double EarnedHours { get; set; }

        public int EarnedMinutes { get; set; }

        public double OvertimeHours { get; set; }

        public int OvertimeMinutes { get; set; }

        public string BioMachineId { get; set; }

        public int DeductedHours { get; set; }

        public int LateMinutes { get; set; }

        public double TotalEarnedHours { get; set; }

        public double ActualEarnedHours { get; set; }

        public bool IsLateComer { get; set; }

        public bool IsCheckOutMissing { get; set; }

        public bool IsNextDay { get; set; }

        public TimeSpan CheckInTime { get; set; }

        public TimeSpan CheckOutTime { get; set; }
    }
}

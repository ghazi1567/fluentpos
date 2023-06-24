using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class ShiftPlannerDto : BaseEntityDto
    {
        public Guid EmployeeId { get; set; }

        public Guid PolicyId { get; set; }

        public DateTime ShiftDate { get; set; }

        public string EmployeeName { get; set; }

        public int? PunchCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsNextDay { get; set; }

        public TimeSpan StartTimeSpan { get; set; }

        public TimeSpan EndTimeSpan { get; set; }

        public string PolicyName { get; set; }
    }
}

using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class OvertimeRequestDto : BaseEntityDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime OvertimeDate { get; set; }

        public int AllowedHours { get; set; }

        public string Status { get; set; }

        public Guid RequestedBy { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsNextDay { get; set; }

        public string EmployeeName { get; set; }

        public int? PunchCode { get; set; }

        public TimeSpan StartTimeSpan { get; set; }

        public TimeSpan EndTimeSpan { get; set; }
    }
}
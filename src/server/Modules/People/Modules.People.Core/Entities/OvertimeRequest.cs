using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class OvertimeRequest : BaseEntity
    {
        public long EmployeeId { get; set; }

        public DateTime OvertimeDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsNextDay { get; set; }

        public int AllowedHours { get; set; }

        public string Status { get; set; }

        public long RequestedBy { get; set; }
    }
}
using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class ShiftPlanner : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid PolicyId { get; set; }

        public DateTime ShiftDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsNextDay { get; set; }
    }
}

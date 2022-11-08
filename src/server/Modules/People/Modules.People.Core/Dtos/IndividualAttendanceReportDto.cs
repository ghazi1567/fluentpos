using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class IndividualAttendanceReportDto
    {
        public Guid EmployeeId { get; set; }

        public AttendanceStatus Sunday { get; set; }

        public AttendanceStatus Monday { get; set; }

        public AttendanceStatus Tuesday { get; set; }

        public AttendanceStatus Wednesday { get; set; }

        public AttendanceStatus Thursday { get; set; }

        public AttendanceStatus Friday { get; set; }

        public AttendanceStatus Saturday { get; set; }
    }
}
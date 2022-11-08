using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class AttendanceDto : Shared.DTOs.Dtos.Peoples.AttendanceDto
    {
        public string EmployeeName { get; set; }
    }
}

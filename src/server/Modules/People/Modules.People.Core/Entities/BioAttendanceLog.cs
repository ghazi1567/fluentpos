using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class BioAttendanceLog : BaseEntity
    {
        public string PersonId { get; set; }

      

        public string Name { get; set; }

        public string Department { get; set; }

        public DateTime AttendanceDateTime { get; set; }

        public string AttendanceStatus { get; set; }

        public string CustomName { get; set; }

        public string AttendanceCheckPoint { get; set; }

        public string DataSource { get; set; }

        public string HandlingType { get; set; }

        public string Temperature { get; set; }

        public string Abnormal { get; set; }

    }
}
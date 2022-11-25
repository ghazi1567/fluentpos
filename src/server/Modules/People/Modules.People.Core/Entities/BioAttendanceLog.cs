using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class BioAttendanceLog : BaseEntity
    {
        public string PunchCode { get; set; }

        public string CardNo { get; set; }

        public string Name { get; set; }

        public DateTime AttendanceDateTime { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime AttendanceTime { get; set; }

        public string Direction { get; set; }

        public string DeviceSerialNo { get; set; }

        public string DeviceName { get; set; }
    }
}
using FluentPOS.Shared.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        public bool IsUsed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogId { get; set; }
    }
}
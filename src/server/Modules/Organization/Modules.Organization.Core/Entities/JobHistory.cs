using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Organization.Core.Entities
{
    public class JobHistory : BaseEntity
    {
        public JobType Job { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Description { get; set; }

        public string TriggeredBy { get; set; }
    }
}
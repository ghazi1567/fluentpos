using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Organization.Core.Dtos
{
    public class JobHistoryDto : BaseEntityDto
    {
        public JobType Job { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Description { get; set; }

        public string TriggeredBy { get; set; }
    }
}
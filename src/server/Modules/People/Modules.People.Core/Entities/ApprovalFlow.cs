using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System.Collections.Generic;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class ApprovalFlow : BaseEntity
    {
        public RequestType FlowType { get; set; }

        public ApprovalType ApprovalType { get; set; }

        public List<ApprovalFlowLevels> Levels { get; set; }
    }
}
using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class ApprovalFlowLevels : BaseEntity
    {
        public long ApprovalFlowId { get; set; }

        public long? ApprovalId { get; set; }

        public int ApprovalIndex { get; set; }

    }
}
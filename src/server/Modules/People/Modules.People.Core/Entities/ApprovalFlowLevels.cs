using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class ApprovalFlowLevels : BaseEntity
    {
        public Guid ApprovalFlowId { get; set; }

        public Guid? ApprovalId { get; set; }

        public int ApprovalIndex { get; set; }

    }
}
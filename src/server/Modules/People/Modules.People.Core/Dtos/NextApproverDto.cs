using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class NextApproverDto
    {
        public bool IsWorkFlowFound { get; set; }

        public NextApproverDto()
        {
            IsWorkFlowFound = true;
        }

        public Guid? ApprovalFlowId { get; set; }

        public Guid ApprovalId { get; set; }

        public int ApprovalIndex { get; set; }
    }
}

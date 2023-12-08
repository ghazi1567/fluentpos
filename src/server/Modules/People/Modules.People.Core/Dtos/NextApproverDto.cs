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

        public long? ApprovalFlowId { get; set; }

        public long ApprovalId { get; set; }

        public int ApprovalIndex { get; set; }
    }
}

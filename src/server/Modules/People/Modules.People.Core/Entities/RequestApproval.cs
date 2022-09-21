using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class RequestApproval : BaseEntity
    {
        public Guid EmployeeRequestId { get; set; }

        public RequestStatus Status { get; set; }

        public string Comments { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public Guid ApproverId { get; set; }

        public int ApprovalIndex { get; set; }

    }
}
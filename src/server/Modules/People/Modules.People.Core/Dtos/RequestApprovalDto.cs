using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class RequestApprovalDto : BaseEntityDto
    {
        public Guid EmployeeRequestId { get; set; }

        public RequestStatus Status { get; set; }

        public string Comments { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public Guid ApproverId { get; set; }

        public int ApprovalIndex { get; set; }

        public string EmployeeName { get; set; }
    }
}
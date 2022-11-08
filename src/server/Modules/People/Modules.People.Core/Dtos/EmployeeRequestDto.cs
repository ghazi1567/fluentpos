using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.People.Core.Dtos
{
    public class EmployeeRequestDto : BaseEntityDto
    {
        public Guid EmployeeId { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PolicyId { get; set; }

        public Guid? DesignationId { get; set; }

        public RequestType RequestType { get; set; }

        public DateTime RequestedOn { get; set; } = DateTime.Now;

        public Guid RequestedBy { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        public int OvertimeHours { get; set; }

        public OverTimeType OverTimeType { get; set; }

        public string Reason { get; set; }

        public RequestStatus Status { get; set; }

        public Guid? AssignedTo { get; set; }

        public DateTime? AssignedOn { get; set; }

        public DateTime? StatusUpdateOn { get; set; }

        public Guid? WorkflowId { get; set; }

        public List<RequestApprovalDto> Approvals { get; set; }

        public string RequestedForName { get; set; }

        public string RequestedByName { get; set; }


        public int Production { get; set; }

        public int RequiredProduction { get; set; }


    }
}

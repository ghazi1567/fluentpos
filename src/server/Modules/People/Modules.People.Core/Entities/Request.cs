using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class EmployeeRequest : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid PolicyId { get; set; }

        public Guid DesignationId { get; set; }

        public RequestType RequestType { get; set; }

        public DateTime RequestedOn { get; set; }

        public Guid RequestedBy { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        public int OvertimeHours { get; set; }

        public OverTimeType OverTimeType { get; set; }

        public string Reason { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public Guid? WorkflowId { get; set; }

        public Guid? AssignedTo { get; set; }

        public DateTime? AssignedOn { get; set; }

        public int Production { get; set; }

        public int RequiredProduction { get; set; }

        public Guid? ModificationId { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
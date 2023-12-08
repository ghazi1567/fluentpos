using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class EmployeeRequest : BaseEntity
    {
        public long EmployeeId { get; set; }

        public long DepartmentId { get; set; }

        public long PolicyId { get; set; }

        public long DesignationId { get; set; }

        public RequestType RequestType { get; set; }

        public DateTime RequestedOn { get; set; }

        public long RequestedBy { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public double OvertimeHours { get; set; }

        public OverTimeType OverTimeType { get; set; }

        public string Reason { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime StatusUpdateOn { get; set; }

        public long? WorkflowId { get; set; }

        public long? AssignedTo { get; set; }

        public DateTime? AssignedOn { get; set; }

        public int Production { get; set; }

        public int RequiredProduction { get; set; }

        public long? ModificationId { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

        public bool IsNextDay { get; set; }
    }
}
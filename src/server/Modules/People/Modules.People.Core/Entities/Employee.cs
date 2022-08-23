using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string FatherName { get; set; }

        public string EmployeeCode { get; set; }

        public int PunchCode { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public bool AllowManualAttendance { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string MaritalStatus { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string FamilyCode { get; set; }

        public string Religion { get; set; }

        public string BankAccountNo { get; set; }

        public string BankAccountTitle { get; set; }

        public string BankName { get; set; }

        public string BankBranchCode { get; set; }

        public string Address { get; set; }

        public string CnicNo { get; set; }

        public DateTime? CnicIssueDate { get; set; }

        public DateTime? CnicExpireDate { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid DesignationId { get; set; }

        public Guid PolicyId { get; set; }

        public string EmployeeStatus { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public DateTime? ResignDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
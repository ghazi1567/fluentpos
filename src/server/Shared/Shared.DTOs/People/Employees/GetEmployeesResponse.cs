using System;

namespace FluentPOS.Shared.DTOs.People.Employees
{
    public record GetEmployeesResponse(Guid Id,

DateTime? CreateaAt,

DateTime? UpdatedAt,

Guid OrganizationId,

Guid BranchId,

Guid UserId,

string IpAddress,
string Prefix,

string FirstName,

string LastName,

string FullName,

string FatherName,

string EmployeeCode,

int? PunchCode,

string MobileNo,

string PhoneNo,

string Email,

bool AllowManualAttendance,

string UserName,

string Password,

string MaritalStatus,

string Gender,

DateTime? DateOfBirth,

string PlaceOfBirth,

string FamilyCode,

string Religion,

string BankAccountNo,

string BankAccountTitle,

string BankName,

string BankBranchCode,

string Address,

string CnicNo,

DateTime? CnicIssueDate,

DateTime? CnicExpireDate,

Guid DepartmentId,

Guid DesignationId,

Guid PolicyId,

string EmployeeStatus,

DateTime? JoiningDate,

DateTime? ConfirmationDate,

DateTime? ContractStartDate,

DateTime? ContractEndDate,

DateTime? ResignDate,

string ImageUrl,

bool Active);
}
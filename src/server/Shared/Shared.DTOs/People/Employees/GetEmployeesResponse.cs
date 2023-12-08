using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.People.Employees
{
    public record GetEmployeesResponse(long Id,

DateTimeOffset? CreatedAt,

DateTimeOffset? UpdatedAt,

long OrganizationId,

long BranchId,

long UserId,

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

long DepartmentId,

long DesignationId,

long PolicyId,

string EmployeeStatus,

DateTime? JoiningDate,

DateTime? ConfirmationDate,

DateTime? ContractStartDate,

DateTime? ContractEndDate,

DateTime? ResignDate,

string ImageUrl,

bool Active,
decimal BasicSalary,
long? ReportingTo,
PaymentMode PaymentMode,
string MotherName,

string City,

string Country,

string EOBINo,

string Qualification,

string BloodGroup,

string Languages,

string SocialSecurityNo,

string NICPlace,

string Domicile,
PayPeriod PayPeriod);
}
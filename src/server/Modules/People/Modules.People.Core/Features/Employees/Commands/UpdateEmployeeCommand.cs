// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands
{
    public class UpdateEmployeeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

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

        public string Station { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public string EmployeeStatus { get; set; }

        public string EmployeeGroup { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public DateTime? ResignDate { get; set; }

        public UploadRequest UploadRequest { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    internal class EmployeeCommandHandler :
        IRequestHandler<RegisterEmployeeCommand, Result<Guid>>,
        IRequestHandler<ImportEmployeeCommand, Result<Guid>>,
        IRequestHandler<RemoveEmployeeCommand, Result<Guid>>,
        IRequestHandler<UpdateEmployeeCommand, Result<Guid>>,
        IRequestHandler<AssignDepartmentCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IPayrollService _payrollService;


        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;

        public EmployeeCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<EmployeeCommandHandler> localizer,
            IDistributedCache cache,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _payrollService = payrollService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = _mapper.Map<Employee>(command);

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"E-{command.FullName}{uploadRequest.Extension}";
                employee.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            }

            // customer.AddDomainEvent(new EmployeeRegisteredEvent(customer));

            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _payrollService.InsertBasicSalary(employee.Id, employee.BasicSalary);
            return await Result<Guid>.SuccessAsync(employee.Id, _localizer["Employee Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.Employees.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (employee != null)
            {
                employee = _mapper.Map<Employee>(command);

                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"E-{command.FullName}{uploadRequest.Extension}";
                    employee.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
                }

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync(cancellationToken);
                await _payrollService.InsertBasicSalary(employee.Id, employee.BasicSalary);
                return await Result<Guid>.SuccessAsync(employee.Id, _localizer["Employee Updated"]);
            }
            else
            {
                throw new PeopleException(_localizer["Employee Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveEmployeeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.Employees.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (employee != null)
            {
                employee.Active = false;
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(employee.Id, _localizer["Employee Updated to InActive"]);
            }
            else
            {
                throw new PeopleException(_localizer["Employee Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(ImportEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employees = _mapper.Map<List<Employee>>(command.Employees);
            int duplicateCount = 0;
            List<Employee> filtered = new List<Employee>();
            foreach (var item in employees)
            {
                bool isEists = await _context.Employees.AnyAsync(x => x.EmployeeCode == item.EmployeeCode || x.PunchCode == item.PunchCode);
                if (!isEists)
                {
                    filtered.Add(item);
                }
                else
                {
                    duplicateCount++;
                }
            }

            await _context.Employees.AddRangeAsync(filtered, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var item in employees)
            {
                await _payrollService.InsertBasicSalary(item.Id, item.BasicSalary);
            }

            var messages = new List<string>();
            messages.Add(_localizer[$"{filtered.Count} Employees Imported Successfully."]);
            messages.Add(_localizer[$"{duplicateCount} Duplicate Employees Found."]);

            return await Result<Guid>.SuccessAsync(Guid.Empty, messages);
        }

        public async Task<Result<Guid>> Handle(AssignDepartmentCommand command, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees.Where(x => command.EmployeeIds.Contains(x.Id)).ToListAsync();

            foreach (var item in employees)
            {
                item.DepartmentId = command.DepartmentId;
                item.UpdatedAt = DateTime.Now;

            }

            _context.Employees.UpdateRange(employees);
            _context.SaveChanges();

            return await Result<Guid>.SuccessAsync(Guid.Empty, $"Department Assigned to {command.EmployeeIds.Count} Employees");
        }

    }
}
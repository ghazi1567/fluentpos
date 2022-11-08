// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Commands
{
    internal class SalaryPerksCommandHandler :
        IRequestHandler<RegisterSalaryPerksCommand, Result<Guid>>,
        IRequestHandler<RemoveSalaryPerksCommand, Result<Guid>>,
        IRequestHandler<UpdateSalaryPerksCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalaryPerksCommandHandler> _localizer;
        private readonly IPayrollService _payrollService;

        public SalaryPerksCommandHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IStringLocalizer<SalaryPerksCommandHandler> localizer,
            IDistributedCache cache,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
            _payrollService = payrollService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterSalaryPerksCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var salary = _context.Salaries.AsNoTracking().FirstOrDefault(x => x.EmployeeId == command.EmployeeId);

            var salaryPerks = _mapper.Map<SalaryPerks>(command);
            await _context.SalaryPerks.AddAsync(salaryPerks, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (command.Type == Shared.DTOs.Enums.SalaryPerksType.Increment)
            {
                await _payrollService.SalaryIncrement(command.EmployeeId, command.Amount);
            }
            else if (command.Type == Shared.DTOs.Enums.SalaryPerksType.Decrement)
            {
                await _payrollService.SalaryDecrement(command.EmployeeId, command.Amount);
            }

            return await Result<Guid>.SuccessAsync(salaryPerks.Id, _localizer["Salary Perks Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateSalaryPerksCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var salaryPerks = await _context.SalaryPerks.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (salaryPerks != null)
            {
                salaryPerks = _mapper.Map<SalaryPerks>(command);

                _context.SalaryPerks.Update(salaryPerks);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(salaryPerks.Id, _localizer["Salary Perks Updated"]);
            }
            else
            {
                throw new AccountingException(_localizer["Salary Perks Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveSalaryPerksCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var salaryPerks = await _context.SalaryPerks.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (salaryPerks != null && (salaryPerks.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives || salaryPerks.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions))
            {
                _context.SalaryPerks.Remove(salaryPerks);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(salaryPerks.Id, _localizer["Salary Perks Deleted"]);
            }
            else
            {
                throw new AccountingException(_localizer["Salary Perks Not Found!"], HttpStatusCode.NotFound);
            }
        }

    }
}
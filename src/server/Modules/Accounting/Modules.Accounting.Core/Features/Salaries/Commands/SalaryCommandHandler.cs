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
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Commands
{
    internal class SalaryCommandHandler :
        IRequestHandler<RegisterSalaryCommand, Result<Guid>>,
        IRequestHandler<RemoveSalaryCommand, Result<Guid>>,
        IRequestHandler<UpdateSalaryCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalaryCommandHandler> _localizer;

        public SalaryCommandHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<SalaryCommandHandler> localizer,
            IDistributedCache cache,
            IEmployeeService employeeService,
            IWorkFlowService workFlowService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterSalaryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var salary = _mapper.Map<Salary>(command);
            await _context.Salaries.AddAsync(salary, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(salary.Id, _localizer["Salary Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateSalaryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var salary = await _context.Salaries.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (salary != null)
            {
                salary = _mapper.Map<Salary>(command);

                _context.Salaries.Update(salary);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(salary.Id, _localizer["Salary Updated"]);
            }
            else
            {
                throw new AccountingException(_localizer["Salary Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveSalaryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var salary = await _context.Salaries.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (salary != null)
            {
                _context.Salaries.Remove(salary);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(salary.Id, _localizer["Salary Deleted"]);
            }
            else
            {
                throw new AccountingException(_localizer["Salary Not Found!"], HttpStatusCode.NotFound);
            }
        }

    }
}
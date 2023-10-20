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
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Commands
{
    internal class PayrollCommandHandler :
        IRequestHandler<RegisterPayrollCommand, Result<Guid>>,
        IRequestHandler<RemovePayrollCommand, Result<Guid>>,
        IRequestHandler<UpdatePayrollCommand, Result<Guid>>,
        IRequestHandler<RunPayrollJobCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PayrollCommandHandler> _localizer;
        private readonly IPayrollService _payrollService;
        private readonly IJobService _jobService;

        public PayrollCommandHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<PayrollCommandHandler> localizer,
            IDistributedCache cache,
            IJobService jobService,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
            _payrollService = payrollService;
            _jobService = jobService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterPayrollCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var payrollRequest = _mapper.Map<PayrollRequest>(command);

            // bool exist = _context.PayrollRequests.Any(x => x.Month == command.Month && x.PayPeriod == command.PayPeriod);
            // if (exist)
            // {
            //     var exists = await _context.PayrollRequests.Where(x => x.Month == command.Month && x.PayPeriod == command.PayPeriod).AsNoTracking().ToListAsync(cancellationToken);
            //     foreach (var item in exists)
            //     {
            //         switch (item.PayPeriod)
            //         {
            //             case Shared.DTOs.Enums.PayPeriod.Daily:
            //                 break;
            //             case Shared.DTOs.Enums.PayPeriod.Weekly:
            //                 break;
            //             case Shared.DTOs.Enums.PayPeriod.HalfMonth:
            //                 if (item.StartDate == command.StartDate && item.EndDate == command.EndDate)
            //                 {
            //                     throw new AccountingException(_localizer["Payroll Request Already Generated For Selected Dates."], HttpStatusCode.Conflict);
            //                 }
            //                 break;
            //             case Shared.DTOs.Enums.PayPeriod.Monthly:
            //                 if (command.StartDate <= item.StartDate || command.EndDate >= item.StartDate)
            //                 {
            //                     throw new AccountingException(_localizer["Payroll Request Already Generated For Selected Dates."], HttpStatusCode.Conflict);
            //                 }
            //                 if (command.StartDate <= item.EndDate || command.EndDate >= item.EndDate)
            //                 {
            //                     throw new AccountingException(_localizer["Payroll Request Already Generated For Selected Dates."], HttpStatusCode.Conflict);
            //                 }
            //                 throw new AccountingException(_localizer["Payroll Request Already Generated For Selected Dates."], HttpStatusCode.Conflict);
            //                 break;
            //             case Shared.DTOs.Enums.PayPeriod.DateRange:
            //                 break;
            //             default:
            //                 break;
            //         }
            //     }
            // }

            await _context.PayrollRequests.AddAsync(payrollRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            command.Id = payrollRequest.Id;
            _jobService.Enqueue(() => _payrollService.GeneratePayroll(command));
            return await Result<Guid>.SuccessAsync(payrollRequest.Id, _localizer["Payroll Request Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdatePayrollCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var payrollRequest = await _context.PayrollRequests.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (payrollRequest != null)
            {
                payrollRequest = _mapper.Map<PayrollRequest>(command);

                _context.PayrollRequests.Update(payrollRequest);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(payrollRequest.Id, _localizer["Payroll Request Updated"]);
            }
            else
            {
                throw new AccountingException(_localizer["Payroll Request Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemovePayrollCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var payrollRequest = await _context.PayrollRequests.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (payrollRequest != null)
            {
                _context.PayrollRequests.Remove(payrollRequest);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(payrollRequest.Id, _localizer["Payroll Request Deleted"]);
            }
            else
            {
                throw new AccountingException(_localizer["Payroll Request Not Found!"], HttpStatusCode.NotFound);
            }
        }



#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RunPayrollJobCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var payrollRequest = await _context.PayrollRequests.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (payrollRequest != null)
            {
                var payrollRequestDto = _mapper.Map<PayrollRequestDto>(payrollRequest);
                payrollRequest.Status = "Running";
                _context.PayrollRequests.Update(payrollRequest);
                await _context.SaveChangesAsync(cancellationToken);
                _jobService.Enqueue(() => _payrollService.GeneratePayroll(payrollRequestDto));
                return await Result<Guid>.SuccessAsync(payrollRequest.Id, _localizer["Payroll Request Updated"]);
            }
            else
            {
                throw new AccountingException(_localizer["Payroll Request Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}
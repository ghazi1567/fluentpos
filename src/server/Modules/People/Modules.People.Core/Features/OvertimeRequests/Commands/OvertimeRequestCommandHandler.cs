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
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.OvertimeRequests.Commands
{
    internal class OvertimeRequestCommandHandler :
        IRequestHandler<RegisterOvertimeRequestCommand, Result<Guid>>,
        IRequestHandler<RemoveOvertimeRequestCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<OvertimeRequestCommandHandler> _localizer;
        private readonly IAttendanceService _attendanceService;
        private readonly IPayrollService _payrollService;

        private readonly IWorkFlowService _workFlowService;

        public OvertimeRequestCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<OvertimeRequestCommandHandler> localizer,
            IDistributedCache cache,
            IAttendanceService attendanceService,
            IWorkFlowService workFlowService,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _attendanceService = attendanceService;
            _workFlowService = workFlowService;
            _payrollService = payrollService;
        }

        public async Task<Result<Guid>> Handle(RemoveOvertimeRequestCommand request, CancellationToken cancellationToken)
        {
            var overtimeRequest = await _context.OvertimeRequests.Where(c => c.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (overtimeRequest != null)
            {
                _context.OvertimeRequests.Remove(overtimeRequest);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(overtimeRequest.Id, _localizer["Overtime request Deleted"]);
            }
            else
            {
                throw new PeopleException(_localizer["Overtime Request Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(RegisterOvertimeRequestCommand request, CancellationToken cancellationToken)
        {
            var overtimeRequest = _mapper.Map<OvertimeRequest>(request);
            await _context.OvertimeRequests.AddAsync(overtimeRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(overtimeRequest.Id, _localizer["Overtime request Saved"]);
        }
    }
}
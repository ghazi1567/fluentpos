using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class WorkFlowService : IWorkFlowService
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAttendanceService _attendanceService;
        private readonly IStringLocalizer<WorkFlowService> _localizer;

        public WorkFlowService(IPeopleDbContext context, IMapper mapper, IAttendanceService attendanceService, IStringLocalizer<WorkFlowService> localizer)
        {
            _context = context;
            _mapper = mapper;
            _attendanceService = attendanceService;
            _localizer = localizer;
        }

        public List<NextApproverDto> GetNextAssignee(Guid requestId, RequestType requestType, Guid employeeId)
        {
            List<NextApproverDto> nextApproverResponse = new List<NextApproverDto>();
            var workFlow = _context.ApprovalFlows.Where(x => x.FlowType == requestType).Include(i => i.Levels).FirstOrDefault();

            if (workFlow == null)
            {
                var employeeDetails = _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
                if (employeeDetails != null && employeeDetails.ReportingTo != null)
                {
                    nextApproverResponse.Add(new NextApproverDto
                    {
                        ApprovalId = employeeDetails.ReportingTo.Value,
                        ApprovalIndex = 1,
                        IsWorkFlowFound = false
                    });
                }
            }
            else
            {
                foreach (var item in workFlow.Levels)
                {
                    nextApproverResponse.Add(new NextApproverDto
                    {
                        ApprovalFlowId = item.ApprovalFlowId,
                        ApprovalId = item.ApprovalId.Value,
                        ApprovalIndex = item.ApprovalIndex,
                        IsWorkFlowFound = true
                    });
                }
            }

            return nextApproverResponse.OrderBy(x => x.ApprovalIndex).ToList();
        }

        public async Task<bool> AssignAprroversAsync(Guid requestId)
        {
            var request = _context.EmployeeRequests.Where(x => x.Id == requestId).FirstOrDefault();
            if (request == null)
            {
                throw new PeopleException(_localizer["Request not found!"], HttpStatusCode.NotFound);
            }

            List<RequestApproval> requestApprovals = new List<RequestApproval>();
            Guid? workflowId = null;
            var approvers = GetNextAssignee(request.Id, request.RequestType, request.EmployeeId);
            if (approvers.Count > 0)
            {
                var firstApprover = approvers.FirstOrDefault();
                if (firstApprover != null)
                {
                    request.AssignedTo = firstApprover.ApprovalId;
                    request.AssignedOn = DateTime.Now;
                    request.Status = RequestStatus.Pending;
                }

                foreach (var item in approvers)
                {
                    if (workflowId == null)
                    {
                        workflowId = item.ApprovalFlowId;
                    }

                    requestApprovals.Add(new RequestApproval
                    {
                        ApprovalIndex = item.ApprovalIndex,
                        ApproverId = item.ApprovalId,
                        Status = RequestStatus.Pending,
                        StatusUpdateOn = DateTime.Now,
                        EmployeeRequestId = requestId,
                        BranchId = request.BranchId,
                        OrganizationId = request.OrganizationId,
                        Id = Guid.NewGuid()
                    });
                }
                if (workflowId != null)
                {
                    request.WorkflowId = workflowId;
                }
            }

            _context.EmployeeRequests.Update(request);
            _context.RequestApprovals.AddRange(requestApprovals);
            await _context.SaveChangesAsync(default(CancellationToken));
            return true;
        }

        public async Task<bool> ApproveRequestAsync(Guid requestId, Guid approverId, string comments)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request == null)
            {
                throw new PeopleException(_localizer["Request not found!"], HttpStatusCode.NotFound);
            }

            var approvals = _context.RequestApprovals.Where(x => x.EmployeeRequestId == requestId).ToList();
            if (approvals.Count > 0)
            {
                var approvalLevel = approvals.Where(x => x.ApproverId == approverId && x.Status != RequestStatus.Approved).OrderBy(x => x.ApprovalIndex).FirstOrDefault();

                if (approvalLevel != null)
                {
                    approvalLevel.Status = RequestStatus.Approved;
                    approvalLevel.Comments = comments;
                    approvalLevel.StatusUpdateOn = DateTime.Now;
                    _context.RequestApprovals.Update(approvalLevel);

                    // await _context.SaveChangesAsync(default(CancellationToken));
                }

                var nextApproval = _context.RequestApprovals.Where(x => x.EmployeeRequestId == requestId && x.ApproverId != approvalLevel.ApproverId && x.Status != RequestStatus.Approved).OrderBy(x => x.ApprovalIndex).FirstOrDefault();
                if (nextApproval != null)
                {
                    request.AssignedTo = nextApproval.ApproverId;
                    request.StatusUpdateOn = DateTime.Now;
                }
                else
                {
                    request.Status = RequestStatus.Approved;
                    request.StatusUpdateOn = DateTime.Now;
                }
            }
            else
            {
                approvals.Add(new RequestApproval
                {
                    ApprovalIndex = 1,
                    ApproverId = approverId,
                    Status = RequestStatus.Approved,
                    StatusUpdateOn = DateTime.Now,
                    EmployeeRequestId = requestId,
                    BranchId = request.BranchId,
                    OrganizationId = request.OrganizationId,
                    Id = Guid.NewGuid()
                });
                _context.RequestApprovals.AddRange(approvals);

                // await _context.SaveChangesAsync(default(CancellationToken));

                request.Status = RequestStatus.Approved;
                request.StatusUpdateOn = DateTime.Now;
            }

            _context.EmployeeRequests.Update(request);

            if (request.Status == RequestStatus.Approved)
            {
                if (request.RequestType == RequestType.Attendance)
                {
                    await _attendanceService.MarkManualAttendance(requestId);
                }
                else if (request.RequestType == RequestType.OverTime)
                {
                    await _attendanceService.MarkOverTime(requestId);
                }
                else if (request.RequestType == RequestType.OverTimeModify || request.RequestType == RequestType.AttendanceModify)
                {
                    await _attendanceService.UpdateModification(requestId);
                }
            }

            await _context.SaveChangesAsync(default(CancellationToken));
            return true;
        }

        public async Task<bool> RejectRequestAsync(Guid requestId, Guid approverId, string comments)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request == null)
            {
                throw new PeopleException(_localizer["Request not found!"], HttpStatusCode.NotFound);
            }

            var approvals = _context.RequestApprovals.Where(x => x.EmployeeRequestId == requestId).ToList();
            if (approvals.Count > 0)
            {
                var approvalLevel = approvals.Where(x => x.ApproverId == approverId && x.Status != RequestStatus.Approved).OrderBy(x => x.ApprovalIndex).FirstOrDefault();

                if (approvalLevel != null)
                {
                    approvalLevel.Status = RequestStatus.Rejected;
                    approvalLevel.Comments = comments;
                    approvalLevel.StatusUpdateOn = DateTime.Now;
                    _context.RequestApprovals.Update(approvalLevel);

                    // await _context.SaveChangesAsync(default(CancellationToken));
                }

                request.Status = RequestStatus.Rejected;
                request.StatusUpdateOn = DateTime.Now;
            }
            else
            {
                approvals.Add(new RequestApproval
                {
                    ApprovalIndex = 1,
                    ApproverId = approverId,
                    Status = RequestStatus.Rejected,
                    StatusUpdateOn = DateTime.Now,
                    EmployeeRequestId = requestId,
                    BranchId = request.BranchId,
                    OrganizationId = request.OrganizationId,
                    Id = Guid.NewGuid()
                });
                _context.RequestApprovals.AddRange(approvals);

                // await _context.SaveChangesAsync(default(CancellationToken));

                request.Status = RequestStatus.Rejected;
                request.StatusUpdateOn = DateTime.Now;
            }

            _context.EmployeeRequests.Update(request);
            await _context.SaveChangesAsync(default(CancellationToken));
            return true;
        }
    }
}
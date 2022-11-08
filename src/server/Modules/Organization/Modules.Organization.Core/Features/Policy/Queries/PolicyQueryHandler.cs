// --------------------------------------------------------------------------------------------------
// <copyright file="BrandQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Organizations.Policies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features
{
    internal class PolicyQueryHandler :
        IRequestHandler<GetPoliciesQuery, PaginatedResult<GetPolicyResponse>>,
        IRequestHandler<GetPolicyByIdQuery, Result<GetPolicyByIdResponse>>
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PolicyQueryHandler> _localizer;

        public PolicyQueryHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<PolicyQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetPolicyResponse>> Handle(GetPoliciesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Policy, GetPolicyResponse>> expression = e => new GetPolicyResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId, e.Name, e.DepartmentId, e.PayslipType, e.PayPeriod, e.AllowedOffDays, e.RequiredWorkingHour, e.ShiftStartTime, e.ShiftEndTime, e.AllowedLateMinutes, e.AllowedLateMinInMonth, e.EarlyArrivalPolicy, e.ForceTimeout, e.TimeoutPolicy, e.IsMonday, e.IsTuesday, e.IsWednesday, e.IsThursday, e.IsFriday, e.IsSaturday, e.IsSunday, e.DailyOverTime, e.HolidayOverTime,e.lateComersPenaltyType,e.lateComersPenalty,e.DailyOverTimeRate,e.HolidayOverTimeRate,e.EarnedHourPolicy,e.SandwichLeaveCount,e.DailyWorkingHour);
            var queryable = _context.Policies.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            var brandList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (brandList == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetPolicyResponse>>(brandList);
        }

        public async Task<Result<GetPolicyByIdResponse>> Handle(GetPolicyByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Policies.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)} Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetPolicyByIdResponse>(entity);
            return await Result<GetPolicyByIdResponse>.SuccessAsync(mappedBrand);
        }


    }
}
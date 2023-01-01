﻿using AutoMapper;
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Infrastructure.Services
{
    public class OrgService : IOrgService
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<OrgService> _localizer;

        public OrgService(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<OrgService> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public OrgDetailsDto GetOrgDetails(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrgDetailsDto> GetPolicyDetailsAsync(Guid Id, bool IncludeAllDetails=false)
        {
            OrgDetailsDto orgDetailsDto = new OrgDetailsDto();

            var policy = await _context.Policies.AsNoTracking()
                .Where(b => b.Id == Id)
                .FirstOrDefaultAsync(default(CancellationToken));

            if (policy != null)
            {
                if (IncludeAllDetails)
                {
                    orgDetailsDto.Organization = await GetOrgnizationDetailsAsync(policy.OrganizationId);
                    orgDetailsDto.Branch = await GetBranchDetailsAsync(policy.BranchId);
                }

                orgDetailsDto.Policy = _mapper.Map<PolicyDto>(policy);
            }

            return orgDetailsDto;
        }

        public async Task<List<PolicyDto>> GetPolicyDetailsAsync(PayPeriod payPeriod)
        {
            var policy = await _context.Policies.AsNoTracking()
                .Where(b => b.PayPeriod == payPeriod)
                .ToListAsync(default(CancellationToken));

            return _mapper.Map<List<PolicyDto>>(policy);
        }

        private async Task<OrganizationDto> GetOrgnizationDetailsAsync(Guid Id)
        {
            var organisation = await _context.Organisations.AsNoTracking()
               .Where(b => b.Id == Id)
               .FirstOrDefaultAsync(default(CancellationToken));

            if (organisation == null)
            {
                return null;
            }

            return new OrganizationDto
            {
                Id = Id,
                Name = organisation.Name,
            };
        }

        private async Task<BranchDto> GetBranchDetailsAsync(Guid Id)
        {
            var branch = await _context.Branchs.AsNoTracking()
               .Where(b => b.Id == Id)
               .FirstOrDefaultAsync(default(CancellationToken));

            if (branch == null)
            {
                return null;
            }

            return new BranchDto
            {
                Id = Id,
                Name = branch.Name,
                OrganizationId  = branch.OrganizationId
            };
        }
    }
}
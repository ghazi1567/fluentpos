// --------------------------------------------------------------------------------------------------
// <copyright file="BrandCommandHandler.cs" company="FluentPOS">
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
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organization.Core.Features
{
    internal class PolicyCommandHandler :
        IRequestHandler<RemovePolicyCommand, Result<long>>,
        IRequestHandler<RegisterPolicyCommand, Result<long>>,
        IRequestHandler<UpdatePolicyCommand, Result<long>>
    {
        private readonly IDistributedCache _cache;
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<PolicyCommandHandler> _localizer;

        public PolicyCommandHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<PolicyCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(RegisterPolicyCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Policies.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)}  with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var mappedEntity = _mapper.Map<Policy>(command);
            mappedEntity.CreatedAt = DateTime.Now;
            await _context.Policies.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<long>.SuccessAsync(default(long), _localizer[$"{nameof(Policy)} Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(RemovePolicyCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO : check Policy already in use

            var entity = await _context.Policies.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)}  Not Found"], HttpStatusCode.NotFound);
            }

            _context.Policies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<long>.SuccessAsync(default(long), _localizer[$"{nameof(Policy)} Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(UpdatePolicyCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Policies.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)}  Not Found!"], HttpStatusCode.NotFound);
            }

            if (await _context.Policies.AnyAsync(c => c.Id != command.Id && c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Policy)} with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var updatedEntity = _mapper.Map<Policy>(command);
            updatedEntity.CreatedAt = entity.CreatedAt;
            updatedEntity.UpdatedAt = DateTime.Now;

            _context.Policies.Update(updatedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<long>.SuccessAsync(default(long), _localizer[$"{nameof(Policy)} Updated"]);
        }
    }
}
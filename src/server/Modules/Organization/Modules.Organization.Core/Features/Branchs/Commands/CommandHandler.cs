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

namespace FluentPOS.Modules.Organization.Core.Features.Branchs.Commands
{
    internal class CommandHandler :
        IRequestHandler<RemoveBranchCommand, Result<Guid>>,
        IRequestHandler<RegisterBranchCommand, Result<Guid>>,
        IRequestHandler<UpdateBranchCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<CommandHandler> _localizer;

        public CommandHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<CommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterBranchCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Branchs.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Branch)}  with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var mappedEntity = _mapper.Map<Branch>(command);
            mappedEntity.CreateaAt = DateTime.Now;
            await _context.Branchs.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(mappedEntity.Id, _localizer[$"{nameof(Branch)} Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveBranchCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO : check branch already in use

            var entity = await _context.Branchs.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Branch)}  Not Found"], HttpStatusCode.NotFound);
            }

            _context.Branchs.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(entity.Id, _localizer[$"{nameof(Branch)} Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Branchs.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Branch)}  Not Found!"], HttpStatusCode.NotFound);
            }

            if (await _context.Branchs.AnyAsync(c => c.Id != command.Id && c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Branch)} with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var updatedEntity = _mapper.Map<Branch>(command);
            updatedEntity.CreateaAt = entity.CreateaAt;
            updatedEntity.UpdatedAt = DateTime.Now;

            _context.Branchs.Update(updatedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(updatedEntity.Id, _localizer[$"{nameof(Branch)} Updated"]);
        }
    }
}
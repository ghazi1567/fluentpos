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

namespace FluentPOS.Modules.Organization.Core.Features.Organizations.Commands
{
    internal class CommandHandler :
        IRequestHandler<RemoveOrganizationCommand, Result<Guid>>,
        IRequestHandler<RegisterOrganizationCommand, Result<Guid>>,
        IRequestHandler<UpdateOrganizationCommand, Result<Guid>>
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
        public async Task<Result<Guid>> Handle(RegisterOrganizationCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Organisations.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Organisation)}  with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var mappedEntity = _mapper.Map<Organisation>(command);
            mappedEntity.CreatedAt = DateTime.Now;
            await _context.Organisations.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(mappedEntity.Id, _localizer[$"{nameof(Organisation)} Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveOrganizationCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO : check Organisation already in use

            var entity = await _context.Organisations.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Organisation)}  Not Found"], HttpStatusCode.NotFound);
            }

            _context.Organisations.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(entity.Id, _localizer[$"{nameof(Organisation)} Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Organisations.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Organisation)}  Not Found!"], HttpStatusCode.NotFound);
            }

            if (await _context.Organisations.AnyAsync(c => c.Id != command.Id && c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Organisation)} with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var updatedEntity = _mapper.Map<Organisation>(command);
            updatedEntity.CreatedAt = entity.CreatedAt;
            updatedEntity.UpdatedAt = DateTime.Now;

            _context.Organisations.Update(updatedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(updatedEntity.Id, _localizer[$"{nameof(Organisation)} Updated"]);
        }
    }
}
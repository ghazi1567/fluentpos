﻿// --------------------------------------------------------------------------------------------------
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
    internal class DepartmentCommandHandler :
        IRequestHandler<RemoveDepartmentCommand, Result<Guid>>,
        IRequestHandler<RegisterDepartmentCommand, Result<Guid>>,
        IRequestHandler<UpdateDepartmentCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<DepartmentCommandHandler> _localizer;

        public DepartmentCommandHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<DepartmentCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterDepartmentCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Departments.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)}  with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var mappedEntity = _mapper.Map<Department>(command);
            mappedEntity.CreatedAt = DateTime.Now;
            await _context.Departments.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(mappedEntity.UUID, _localizer[$"{nameof(Department)} Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveDepartmentCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO : check Department already in use

            var entity = await _context.Departments.FirstOrDefaultAsync(b => b.UUID == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)}  Not Found"], HttpStatusCode.NotFound);
            }

            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(entity.UUID, _localizer[$"{nameof(Department)} Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Departments.Where(b => b.UUID == command.UUID).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)}  Not Found!"], HttpStatusCode.NotFound);
            }

            if (await _context.Departments.AnyAsync(c => c.UUID != command.UUID && c.Name == command.Name, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)} with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var updatedEntity = _mapper.Map<Department>(command);
            updatedEntity.CreatedAt = entity.CreatedAt;
            updatedEntity.UpdatedAt = DateTime.Now;

            _context.Departments.Update(updatedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(updatedEntity.UUID, _localizer[$"{nameof(Department)} Updated"]);
        }
    }
}
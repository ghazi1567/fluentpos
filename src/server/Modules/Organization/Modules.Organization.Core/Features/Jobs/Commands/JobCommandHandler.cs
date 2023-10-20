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
    internal class JobCommandHandler :
        IRequestHandler<RemoveJobCommand, Result<Guid>>,
        IRequestHandler<RegisterJobCommand, Result<Guid>>,
        IRequestHandler<UpdateJobCommand, Result<Guid>>,
        IRequestHandler<RunJobCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<JobCommandHandler> _localizer;
        private readonly IJobService _jobService;

        public JobCommandHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<JobCommandHandler> localizer,
            IDistributedCache cache,
            IJobService jobService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _jobService = jobService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterJobCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Jobs.AnyAsync(c => c.JobName == command.JobName, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Job)}  with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var mappedEntity = _mapper.Map<Job>(command);
            mappedEntity.CreatedAt = DateTime.Now;
            await _context.Jobs.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            //_jobService.ConfigureJob(mappedEntity.JobName, mappedEntity.Schedule);
            return await Result<Guid>.SuccessAsync(mappedEntity.Id, _localizer[$"{nameof(Designation)} Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveJobCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO : check Designation already in use

            var entity = await _context.Jobs.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Designation)}  Not Found"], HttpStatusCode.NotFound);
            }

            _context.Jobs.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(entity.Id, _localizer[$"{nameof(Designation)} Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateJobCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Jobs.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Job)}  Not Found!"], HttpStatusCode.NotFound);
            }

            if (await _context.Jobs.AnyAsync(c => c.Id != command.Id && c.JobName == command.JobName, cancellationToken))
            {
                throw new OrganizationException(_localizer[$"{nameof(Job)} with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var updatedEntity = _mapper.Map<Job>(command);
            updatedEntity.CreatedAt = entity.CreatedAt;
            updatedEntity.UpdatedAt = DateTime.Now;

            _context.Jobs.Update(updatedEntity);
            await _context.SaveChangesAsync(cancellationToken);
            //_jobService.ConfigureJob(updatedEntity.JobName, updatedEntity.Schedule);
            return await Result<Guid>.SuccessAsync(updatedEntity.Id, _localizer[$"{nameof(Designation)} Updated"]);
        }

        public async Task<Result<Guid>> Handle(RunJobCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Jobs.Where(b => b.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (entity != null)
            {
                //_jobService.RunJob(entity.JobName, command.date);

                return await Result<Guid>.SuccessAsync(entity.Id, _localizer[$"{nameof(Job)} run successfuly"]);
            }

            return await Result<Guid>.FailAsync(_localizer[$"{nameof(Job)} not found."]);
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.PO.Service;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{
    internal sealed class POCommandHandler :
        IRequestHandler<RegisterPOCommand, Result<Guid>>,
         IRequestHandler<RemovePOCommand, Result<Guid>>,
         IRequestHandler<UpdatePOCommand, Result<Guid>>
    {
        private readonly IPOService _poService;

        public POCommandHandler(IPOService poService)
        {
            _poService = poService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterPOCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            return await _poService.Save(command, cancellationToken);
        }

        public async Task<Result<Guid>> Handle(RemovePOCommand request, CancellationToken cancellationToken)
        {
            return await _poService.Delete(request, cancellationToken);
        }

        public async Task<Result<Guid>> Handle(UpdatePOCommand request, CancellationToken cancellationToken)
        {
            return await _poService.Update(request, cancellationToken);
        }
    }
}
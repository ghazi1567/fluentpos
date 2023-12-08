// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Warehouses.Commands
{
    internal sealed class LocationCommandHandler :
        IRequestHandler<RegisterLocationCommand, Result<long>>
    {
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<LocationCommandHandler> _localizer;
        private readonly IMapper _mapper;

        public LocationCommandHandler(
            IStringLocalizer<LocationCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService,
            IMapper mapper,
            IShopifyOrderService shopifyOrderService)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _mapper = mapper;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(RegisterLocationCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var isExist = await _salesContext.Warehouses.SingleOrDefaultAsync(x => x.ShopifyId == command.ShopifyId);
            if (isExist != null)
            {
                return await Result<long>.ReturnErrorAsync(string.Format(_localizer["Duplicate: Warehouse already exists {0}"], command.ShopifyId));
            }

            var warehouse = _mapper.Map<Warehouse>(command);

            await _salesContext.Warehouses.AddAsync(warehouse, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);

            return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["Warehouse {0} Created"], warehouse.ShopifyId));
        }
    }
}
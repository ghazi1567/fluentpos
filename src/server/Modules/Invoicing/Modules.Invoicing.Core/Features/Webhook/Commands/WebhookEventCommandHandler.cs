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
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    internal sealed class WebhookEventCommandHandler :
        IRequestHandler<RegisterWebhookEventCommand, Result<long>>
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<WebhookEventCommandHandler> _localizer;
        private readonly IMapper _mapper;

        public WebhookEventCommandHandler(
            IStringLocalizer<WebhookEventCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService,
            IMapper mapper)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
            _stockService = stockService;
            _referenceService = referenceService;
            _mapper = mapper;
        }

        public Task<Result<long>> Handle(RegisterWebhookEventCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
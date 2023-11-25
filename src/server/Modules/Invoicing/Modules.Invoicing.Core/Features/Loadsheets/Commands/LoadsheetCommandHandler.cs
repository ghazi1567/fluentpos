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
using FluentPOS.Shared.Core.IntegrationServices.Logistics;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Dtos.Logistics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    internal sealed class LoadsheetCommandHandler :
        IRequestHandler<RegisterloadsheetCommand, Result<Guid>>
    {
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<WebhookEventCommandHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IPostexService _postexService;

        public LoadsheetCommandHandler(
            IStringLocalizer<WebhookEventCommandHandler> localizer,
            ISalesDbContext salesContext,
            IMapper mapper,
            IPostexService postexService)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _mapper = mapper;
            _postexService = postexService;
        }

        public async Task<Result<Guid>> Handle(RegisterloadsheetCommand request, CancellationToken cancellationToken)
        {
            var loadsheet = _mapper.Map<LoadSheetMain>(request);

            var foIds = request.Details.Select(x => x.FulfillmentOrderId).ToList();
            var fulfillmentOrders = await _salesContext.FulfillmentOrders.Where(x => foIds.Contains(x.ShopifyId.Value)).ToListAsync();


            foreach (var order in fulfillmentOrders)
            {
                order.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Shipped;
            }

            _salesContext.FulfillmentOrders.UpdateRange(fulfillmentOrders);
            await _salesContext.LoadSheetMains.AddAsync(loadsheet);
            await _salesContext.SaveChangesAsync();

            var warehouseIds = fulfillmentOrders.Select(x => x.WarehouseId.Value).ToList();
            var warehouses = await _salesContext.Warehouses.Where(x => warehouseIds.Contains(x.Id)).ToListAsync();

            List<PostexLoadSheetModel> postexLoadSheetModels = new List<PostexLoadSheetModel>();
            foreach (var item in warehouses)
            {
                postexLoadSheetModels.Add(new PostexLoadSheetModel
                {
                    CityName = item.City,
                    ContactNumber = item.City,
                    PickupAddress = item.Phone,
                    TrackingNumbers = fulfillmentOrders.Where(x => x.WarehouseId == item.Id).Select(x => x.TrackingNumber).ToList()
                });
            }

            foreach (var model in postexLoadSheetModels)
            {
                var response = await _postexService.GenerateLoadsheetAsync(model);
            }

            return await Result<Guid>.SuccessAsync(loadsheet.Id, string.Format(_localizer["Loadsheet generated successfully"]));
        }
    }
}
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    internal sealed class LoadsheetCommandHandler :
        IRequestHandler<RegisterloadsheetCommand, Result<Guid>>,
        IRequestHandler<ReGenerateloadsheetCommand, Result<Guid>>
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

            if (request.UpdateOrderStatus)
            {
                var foIds = request.Details.Select(x => x.FulfillmentOrderId).ToList();
                var fulfillmentOrders = await _salesContext.FulfillmentOrders.Where(x => foIds.Contains(x.ShopifyId.Value)).ToListAsync();
                foreach (var order in fulfillmentOrders)
                {
                    order.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Shipped;
                }

                _salesContext.FulfillmentOrders.UpdateRange(fulfillmentOrders);
            }

            await _salesContext.LoadSheetMains.AddAsync(loadsheet);
            await _salesContext.SaveChangesAsync();

            var postexModel = new PostexLoadSheetModel
            {
                CityName = loadsheet.CityName,
                ContactNumber = loadsheet.ContactNumber,
                PickupAddress = loadsheet.PickupAddress,
                TrackingNumbers = loadsheet.Details.Select(x => x.TrackingNumber).ToList()
            };
            var response = await _postexService.GenerateLoadsheetAsync(postexModel);

            if (response.StatusCode == "200")
            {
                loadsheet.Status = "Generated";
            }
            else
            {
                loadsheet.Status = "Failed";
                loadsheet.Note = response.StatusMessage;
            }

            _salesContext.LoadSheetMains.Update(loadsheet);
            await _salesContext.SaveChangesAsync();

            return await Result<Guid>.SuccessAsync(loadsheet.Id, string.Format(_localizer["Loadsheet generated successfully"]));
        }

        public async Task<Result<Guid>> Handle(ReGenerateloadsheetCommand request, CancellationToken cancellationToken)
        {
            var loadsheet = await _salesContext.LoadSheetMains.Include(x => x.Details).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (loadsheet == null)
            {
                return Result<Guid>.ReturnError(string.Format(_localizer["Loadsheet not found."]));
            }

            var postexModel = new PostexLoadSheetModel
            {
                CityName = loadsheet.CityName,
                ContactNumber = loadsheet.ContactNumber,
                PickupAddress = loadsheet.PickupAddress,
                TrackingNumbers = loadsheet.Details.Select(x => x.TrackingNumber).ToList()
            };
            var response = await _postexService.GenerateLoadsheetAsync(postexModel);

            if (response.StatusCode == "200")
            {
                loadsheet.Status = "Generated";
                loadsheet.Note = "";
            }
            else
            {
                loadsheet.Status = "Failed";
                loadsheet.Note = response.StatusMessage;
            }

            _salesContext.LoadSheetMains.Update(loadsheet);
            await _salesContext.SaveChangesAsync();

            return await Result<Guid>.SuccessAsync(loadsheet.Id, string.Format(_localizer["Loadsheet generated successfully"]));
        }


    }
}
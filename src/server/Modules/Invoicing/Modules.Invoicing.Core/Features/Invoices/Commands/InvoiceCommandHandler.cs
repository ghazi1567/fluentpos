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
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands
{
    internal sealed class InvoiceCommandHandler :
        IRequestHandler<RegisterInvoiceCommand, Result<bool>>,
        IRequestHandler<CloseInvoiceCommand, Result<bool>>
    {
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<InvoiceCommandHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IPostexService _postexService;
        private readonly IShopifyOrderService _shopifyOrderService;

        public InvoiceCommandHandler(
            IStringLocalizer<InvoiceCommandHandler> localizer,
            ISalesDbContext salesContext,
            IMapper mapper,
            IPostexService postexService,
            IShopifyOrderService shopifyOrderService)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _mapper = mapper;
            _postexService = postexService;
            _shopifyOrderService = shopifyOrderService;
        }

        public async Task<Result<bool>> Handle(RegisterInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = _mapper.Map<Invoice>(request);
            var trackingNumbers = invoice.InvoiceDetails.Select(x => x.TrackingNumber).ToList();
            var orders = await _salesContext.FulfillmentOrders.Where(x => trackingNumbers.Contains(x.TrackingNumber)).Select(x => new InternalFulfillmentOrder
            {
                TrackingNumber = x.TrackingNumber,
                TotalPrice = x.TotalPrice,
                OrderStatus = x.OrderStatus,
                Id = x.Id,
            }).ToListAsync();

            foreach (var invoiceDetail in invoice.InvoiceDetails)
            {
                var order = orders.FirstOrDefault(x => x.TrackingNumber == invoiceDetail.TrackingNumber);
                if (order != null)
                {
                    invoiceDetail.FulfillmentOrderId = order.Id;
                    if (invoiceDetail.Status == "Delivered")
                    {
                        invoiceDetail.IsValid = order.TotalPrice.Value == invoiceDetail.CODAmount;
                    }

                    if (invoiceDetail.Status == "Return")
                    {
                        invoiceDetail.IsValid = order.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Returned;
                    }
                }
            }

            invoice.InvalidCount = invoice.InvoiceDetails.Where(x => x.IsValid == false).Count();
            invoice.IsValid = invoice.InvalidCount == 0;
            invoice.TotalDelivered = invoice.InvoiceDetails.Where(x => x.Status == "Delivered").Count();
            invoice.TotalReturned = invoice.InvoiceDetails.Where(x => x.Status == "Return").Count();
            invoice.ToTalCODAmount = invoice.InvoiceDetails.Sum(x => x.CODAmount);
            invoice.TotalNetAmount = invoice.InvoiceDetails.Sum(x => x.NetAmount);
            invoice.TotalReceivable = invoice.InvoiceDetails.Sum(x => x.NetAmountReceivable);
            invoice.TotalReserveAmount = invoice.InvoiceDetails.Sum(x => x.ReserveAmount);
            invoice.TotalShippingCharges = invoice.InvoiceDetails.Sum(x => x.ShippingCharges);
            invoice.TotalTax = invoice.InvoiceDetails.Sum(x => x.Tax);

            await _salesContext.Invoices.AddAsync(invoice);
            await _salesContext.SaveChangesAsync();

            return await Result<bool>.SuccessAsync(true, string.Format(_localizer["Loadsheet generated successfully"]));
        }

        public async Task<Result<bool>> Handle(CloseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _salesContext.Invoices.Include(x => x.InvoiceDetails).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (invoice == null)
            {
                return Result<bool>.ReturnError(string.Format(_localizer["Invoice not found"]));
            }

            var foIds = invoice.InvoiceDetails.Where(x => x.IsValid).Select(x => x.FulfillmentOrderId).ToList();
            var fulfillmentOrders = await _salesContext.FulfillmentOrders.Where(x => foIds.Contains(x.Id)).ToListAsync();

            foreach (var fulfillmentOrder in fulfillmentOrders)
            {
                var invoiceDetail = invoice.InvoiceDetails.FirstOrDefault(x => x.FulfillmentOrderId == fulfillmentOrder.Id);
                if (invoiceDetail != null)
                {
                    if (invoiceDetail.Status == "Delivered")
                    {
                        var result = await _shopifyOrderService.MarkOrderAsPaid(fulfillmentOrder.OrderShopifyId.Value);
                        if (result != null)
                        {
                            fulfillmentOrder.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Paid;
                        }
                    }
                    else if (invoiceDetail.Status == "Return")
                    {
                        await _shopifyOrderService.CloseOrder(fulfillmentOrder.OrderShopifyId.Value);
                        fulfillmentOrder.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Closed;
                    }
                }
            }

            invoice.IsClosed = true;
            invoice.Comments = request.Comments;

            _salesContext.Invoices.Update(invoice);
            _salesContext.FulfillmentOrders.UpdateRange(fulfillmentOrders);
            _salesContext.SaveChanges();
            return await Result<bool>.SuccessAsync(true, string.Format(_localizer["Loadsheet generated successfully"]));

        }
    }
}
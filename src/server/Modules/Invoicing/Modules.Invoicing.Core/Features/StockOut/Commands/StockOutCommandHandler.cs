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
using FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Interfaces.Services.Identity;
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
    internal sealed class StockOutCommandHandler :
        IRequestHandler<RegisterStockOutCommand, Result<long>>,
        IRequestHandler<ApproveStockOutCommand, Result<long>>,
        IRequestHandler<RemoveStockOutCommand, Result<long>>,
        IRequestHandler<UpdateStockOutCommand, Result<long>>
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<StockInCommandHandler> _localizer;
        private readonly ICurrentUser _user;

        public StockOutCommandHandler(
            IStringLocalizer<StockInCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService,
            ICurrentUser user)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
            _stockService = stockService;
            _referenceService = referenceService;
            _user = user;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(RegisterStockOutCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var order = InternalOrder.InitializeOrder(command.TimeStamp);
            string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
            order.SetReferenceNumber(referenceNumber);
            //order.OrderType = OrderType.StockOut;
            order.WarehouseId = command.WarehouseId;
            order.SetNote(command.Note); 
            foreach (var item in command.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                }
            }

            await _salesContext.Orders.AddAsync(order, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);

           

            return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["Stock Out {0} Created for approval"], order.ReferenceNumber));
        }

        public async Task<Result<long>> Handle(ApproveStockOutCommand request, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.AsNoTracking()
               .OrderBy(x => x.TimeStamp)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }

            var po = await _salesContext.PurchaseOrders.AsNoTracking()
              .SingleOrDefaultAsync(x => x.ReferenceNumber == order.ReferenceNumber);

            order.Status = request.Status;
        

            po.Status = request.Status;
            po.ApproveDate = DateTime.Now;
            po.ApproveBy = _user.GetUserId();

            _salesContext.Orders.Update(order);
            _salesContext.PurchaseOrders.Update(po);
            await _salesContext.SaveChangesAsync(cancellationToken);

            if (request.Status == OrderStatus.Pending)
            {
                //foreach (var product in order.Products)
                //{
                //    await _stockService.RecordTransaction(product.ProductId, product.Quantity, order.ReferenceNumber, false);
                //}
            }
            return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["Order {0} {1}"], order.ReferenceNumber, Enum.GetName(typeof(OrderStatus), request.Status)));
        }

        public async Task<Result<long>> Handle(RemoveStockOutCommand request, CancellationToken cancellationToken)
        {
            var stockOut = await _salesContext.Orders
                .Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockOut != null)
            {
                _salesContext.Orders.Remove(stockOut);
                await _salesContext.SaveChangesAsync(cancellationToken);


                return await Result<long>.SuccessAsync(default(long), _localizer["Stock Out Deleted"]);
            }
            else
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<long>> Handle(UpdateStockOutCommand request, CancellationToken cancellationToken)
        {
            var stockOut = await _salesContext.Orders
                .Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockOut != null)
            {
                var order = InternalOrder.InitializeOrder(request.TimeStamp);
                order.SetReferenceNumber(stockOut.ReferenceNumber);
               
                //order.OrderType = OrderType.StockOut;
                order.SetNote(request.Note);
                order.WarehouseId = request.WarehouseId;

                foreach (var item in request.Products)
                {
                    var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                    if (productResponse.Succeeded)
                    {
                        var product = productResponse.Data;
                    }
                }

                _salesContext.Orders.Remove(stockOut);
                await _salesContext.Orders.AddAsync(order, cancellationToken);
                await _salesContext.SaveChangesAsync(cancellationToken);


                return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["Stock Out {0} Updated"], order.ReferenceNumber));
            }
            else
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}
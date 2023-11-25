// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
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

namespace FluentPOS.Modules.Invoicing.Core.Features.PO
{
    internal sealed class StockInCommandHandler :
        IRequestHandler<RegisterStockInCommand, Result<Guid>>,
        IRequestHandler<ApproveStockInCommand, Result<Guid>>,
        IRequestHandler<RemoveStockInCommand, Result<Guid>>,
        IRequestHandler<UpdateStockInCommand, Result<Guid>>
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<StockInCommandHandler> _localizer;
        private readonly ICurrentUser _user;

        public StockInCommandHandler(
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
        public async Task<Result<Guid>> Handle(RegisterStockInCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var po = await _salesContext.PurchaseOrders.AsNoTracking()
               .SingleOrDefaultAsync(x => x.ReferenceNumber == command.ReferenceNumber);

            if (po == null)
            {
                throw new SalesException(_localizer["Purchase Order Not Found!"], HttpStatusCode.NotFound);
            }
            else
            {
                if (po.Status != OrderStatus.Pending)
                {
                    throw new SalesException(_localizer["Purchase Order Already Processed!"], HttpStatusCode.Gone);
                }
            }



            var order = InternalOrder.InitializeOrder(command.TimeStamp);
            string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
            order.SetReferenceNumber(referenceNumber);
            // order.SetPOReferenceNumber(command.ReferenceNumber);
            // order.IsApproved = false;
            // order.Status = OrderStatus.PendingApproval;
            // order.OrderType = OrderType.StockIn;
            order.WarehouseId = command.WarehouseId;
            order.SetNote(command.Note);

            foreach (var item in command.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    // order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                }
            }

            po.Status = OrderStatus.InProgress;
            await _salesContext.Orders.AddAsync(order, cancellationToken);
            _salesContext.PurchaseOrders.Update(po);
            await _salesContext.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Stock In {0} Created for approval"], order.ReferenceNumber));
        }

        public async Task<Result<Guid>> Handle(ApproveStockInCommand request, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.AsNoTracking()
               //.Include(x => x.Products)
               .OrderBy(x => x.TimeStamp)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new SalesException(_localizer["Stock In Not Found!"], HttpStatusCode.NotFound);
            }

            var po = await _salesContext.PurchaseOrders.AsNoTracking()
              .SingleOrDefaultAsync(x => x.ReferenceNumber == order.ReferenceNumber);

            order.Status = request.Status;
            //order.ApprovedDate = DateTime.Now;
            //order.ApprovedBy = _user.GetUserEmail();
            po.Status = request.Status;
            po.ApproveDate = DateTime.Now;
            po.ApproveBy = _user.GetUserId();

            _salesContext.Orders.Update(order);
            _salesContext.PurchaseOrders.Update(po);
            await _salesContext.SaveChangesAsync(cancellationToken);

            if (request.Status == OrderStatus.Approved)
            {
                //foreach (var orderItem in order.Products)
                //{
                //    var productResponse = await _productService.GetDetailsAsync(orderItem.ProductId);
                //    if (productResponse.Succeeded)
                //    {
                //        var product = productResponse.Data;
                //        var factorDate = product.FactorUpdateOn.HasValue ? product.FactorUpdateOn.Value : order.TimeStamp;
                //        await _stockService.RecordTransaction(product.Id, orderItem.Quantity, order.ReferenceNumber, OrderType.StockIn, product.discountFactor, product.Cost, factorDate, order.WarehouseId);
                //    }
                //}
            }

            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Order {0} {1}"], order.ReferenceNumber, Enum.GetName(typeof(OrderStatus), request.Status)));
        }

        public async Task<Result<Guid>> Handle(RemoveStockInCommand request, CancellationToken cancellationToken)
        {
            var stockIn = await _salesContext.Orders.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockIn != null)
            {
                if (stockIn.Status == OrderStatus.Approved)
                {
                    throw new SalesException(_localizer["Stock In Already Approved. Can't able to delete!"], HttpStatusCode.Gone);
                }

                _salesContext.Orders.Remove(stockIn);
                await _salesContext.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(stockIn.Id, _localizer["Stock In Deleted"]);
            }
            else
            {
                throw new SalesException(_localizer["Stock In Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(UpdateStockInCommand request, CancellationToken cancellationToken)
        {
            var stockIn = await _salesContext.Orders.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockIn != null)
            {
                var order = InternalOrder.InitializeOrder(request.TimeStamp);
                order.SetReferenceNumber(stockIn.ReferenceNumber);
                //order.SetPOReferenceNumber(stockIn.POReferenceNo);
                //order.IsApproved = false;
                //order.Status = OrderStatus.PendingApproval;
                //order.OrderType = OrderType.StockIn;
                order.SetNote(request.Note);
                order.WarehouseId = request.WarehouseId;

                foreach (var item in request.Products)
                {
                    var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                    if (productResponse.Succeeded)
                    {
                        var product = productResponse.Data;
                        //order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                    }
                }

                _salesContext.Orders.Remove(stockIn);
                await _salesContext.Orders.AddAsync(order, cancellationToken);
                await _salesContext.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Stock In {0} Updated"], order.ReferenceNumber));
            }
            else
            {
                throw new SalesException(_localizer["Stock In Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}
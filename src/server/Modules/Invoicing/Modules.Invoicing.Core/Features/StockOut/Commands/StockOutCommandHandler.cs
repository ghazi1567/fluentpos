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
        IRequestHandler<RegisterStockOutCommand, Result<Guid>>,
        IRequestHandler<ApproveStockOutCommand, Result<Guid>>,
        IRequestHandler<RemoveStockOutCommand, Result<Guid>>,
        IRequestHandler<UpdateStockOutCommand, Result<Guid>>
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
        public async Task<Result<Guid>> Handle(RegisterStockOutCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var order = Order.InitializeOrder(command.TimeStamp);
            string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
            order.SetReferenceNumber(referenceNumber);
            order.IsApproved = true;
            order.Status = OrderStatus.Approved;
            order.OrderType = OrderType.StockOut;
            order.WarehouseId = command.WarehouseId;

            foreach (var item in command.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                }
            }

            await _salesContext.Orders.AddAsync(order, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);

            foreach (var orderItem in order.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(orderItem.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    var factorDate = product.FactorUpdateOn.HasValue ? product.FactorUpdateOn.Value : order.TimeStamp;
                    await _stockService.RecordTransaction(product.Id, orderItem.Quantity, order.ReferenceNumber, OrderType.StockOut, product.discountFactor, product.Cost, factorDate, order.WarehouseId);
                }
            }

            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Stock Out {0} Created for approval"], order.ReferenceNumber));
        }

        public async Task<Result<Guid>> Handle(ApproveStockOutCommand request, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.AsNoTracking()
               .Include(x => x.Products)
               .OrderBy(x => x.TimeStamp)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }

            var po = await _salesContext.PurchaseOrders.AsNoTracking()
              .SingleOrDefaultAsync(x => x.ReferenceNumber == order.POReferenceNo);

            order.Status = request.Status;
            order.ApprovedDate = DateTime.Now;
            order.ApprovedBy = _user.GetUserEmail();

            po.Status = request.Status;
            po.ApproveDate = DateTime.Now;
            po.ApproveBy = _user.GetUserId();

            _salesContext.Orders.Update(order);
            _salesContext.PurchaseOrders.Update(po);
            await _salesContext.SaveChangesAsync(cancellationToken);

            if (request.Status == OrderStatus.Approved)
            {
                foreach (var product in order.Products)
                {
                    await _stockService.RecordTransaction(product.ProductId, product.Quantity, order.ReferenceNumber, false);
                }
            }
            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Order {0} {1}"], order.ReferenceNumber, Enum.GetName(typeof(OrderStatus), request.Status)));
        }

        public async Task<Result<Guid>> Handle(RemoveStockOutCommand request, CancellationToken cancellationToken)
        {
            var stockOut = await _salesContext.Orders
                .Include(x => x.Products)
                .Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockOut != null)
            {
                _salesContext.Orders.Remove(stockOut);
                await _salesContext.SaveChangesAsync(cancellationToken);

                foreach (var orderItem in stockOut.Products)
                {
                    await _stockService.ReverseTransaction(orderItem.ProductId, orderItem.Quantity, stockOut.ReferenceNumber, OrderType.StockOut, stockOut.WarehouseId);
                }

                return await Result<Guid>.SuccessAsync(stockOut.Id, _localizer["Stock Out Deleted"]);
            }
            else
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(UpdateStockOutCommand request, CancellationToken cancellationToken)
        {
            var stockOut = await _salesContext.Orders
                .Include(x => x.Products)
                .Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (stockOut != null)
            {
                var order = Order.InitializeOrder(request.TimeStamp);
                order.SetReferenceNumber(stockOut.ReferenceNumber);
                order.IsApproved = true;
                order.Status = OrderStatus.Approved;
                order.OrderType = OrderType.StockOut;
                order.SetNote(request.Note);
                order.WarehouseId = request.WarehouseId;

                foreach (var item in request.Products)
                {
                    var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                    if (productResponse.Succeeded)
                    {
                        var product = productResponse.Data;
                        order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                    }
                }

                _salesContext.Orders.Remove(stockOut);
                await _salesContext.Orders.AddAsync(order, cancellationToken);
                await _salesContext.SaveChangesAsync(cancellationToken);

                foreach (var orderItem in stockOut.Products)
                {
                    await _stockService.ReverseTransaction(orderItem.ProductId, orderItem.Quantity, stockOut.ReferenceNumber, OrderType.StockOut, order.WarehouseId);
                }

                foreach (var orderItem in order.Products)
                {
                    var productResponse = await _productService.GetDetailsAsync(orderItem.ProductId);
                    if (productResponse.Succeeded)
                    {
                        var product = productResponse.Data;
                        var factorDate = product.FactorUpdateOn.HasValue ? product.FactorUpdateOn.Value : order.TimeStamp;
                        await _stockService.RecordTransaction(product.Id, orderItem.Quantity, order.ReferenceNumber, OrderType.StockOut, product.discountFactor, product.Cost, factorDate, order.WarehouseId);
                    }
                }

                return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Stock Out {0} Updated"], order.ReferenceNumber));
            }
            else
            {
                throw new SalesException(_localizer["Stock Out Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}
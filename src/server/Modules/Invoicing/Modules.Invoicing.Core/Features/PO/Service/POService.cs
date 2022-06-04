using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.PO.Service
{
    public class POService : IPOService
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<POService> _localizer;

        public POService(
            IStringLocalizer<POService> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
            _stockService = stockService;
            _referenceService = referenceService;
        }

        public async Task<Result<Guid>> Save(RegisterPOCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            PurchaseOrder order = null;
            if (command.Id == Guid.Empty)
            {
                order = PurchaseOrder.InitializeOrder();
            }
            else
            {
                order = PurchaseOrder.InitializeOrder(command.Id);
                if (command.TimeStamp.HasValue)
                {
                    order.TimeStamp = command.TimeStamp.Value;
                }

                if (command.CreateaAt.HasValue)
                {
                    order.CreateaAt = command.CreateaAt.Value;
                }

                if (command.UpdatedAt.HasValue)
                {
                    order.UpdatedAt = command.UpdatedAt.Value;
                }
            }

            if (string.IsNullOrEmpty(command.ReferenceNumber))
            {
                string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
                order.SetReferenceNumber(referenceNumber);
            }
            else
            {
                order.SetReferenceNumber(command.ReferenceNumber);
            }

            order.SetWarehouseId(command.WarehouseId);
            order.Status = OrderStatus.Pending;
            order.SetNotes(command.Note);
            order.IsApproved = false;

            foreach (var item in command.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                }
            }

            bool result = await SavePurchaseOrder(order);
            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Order {0} Created"], order.ReferenceNumber));
        }

        public async Task<bool> SavePurchaseOrder(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _salesContext.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> AlreadyExist(Guid id)
        {
            return await _salesContext.PurchaseOrders.AnyAsync(x => x.Id == id);
        }

        public async Task<Result<Guid>> Delete(RemovePOCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var purchaseOrder = await _salesContext.PurchaseOrders.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (purchaseOrder != null)
            {
                var stockIn = await _salesContext.Orders.Where(p => p.POReferenceNo == purchaseOrder.ReferenceNumber).FirstOrDefaultAsync(cancellationToken);

                if (stockIn != null)
                {
                    throw new SalesException(_localizer["Purchase Order Already Processed. Can't able to delete!"], HttpStatusCode.Gone);
                }

                _salesContext.PurchaseOrders.Remove(purchaseOrder);
                await _salesContext.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(purchaseOrder.Id, _localizer["purchase Order Deleted"]);
            }
            else
            {
                throw new SalesException(_localizer["purchase Order Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Update(UpdatePOCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var purchaseOrder = await _salesContext.PurchaseOrders.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (purchaseOrder != null)
            {
                var order = PurchaseOrder.InitializeOrder(purchaseOrder.TimeStamp);
                order.SetReferenceNumber(request.ReferenceNumber);
                order.SetWarehouseId(request.WarehouseId);
                order.Status = OrderStatus.Pending;
                order.SetNotes(request.Note);
                order.IsApproved = false;
                order.CreateaAt = purchaseOrder.CreateaAt;
                foreach (var item in request.Products)
                {
                    var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                    if (productResponse.Succeeded)
                    {
                        var product = productResponse.Data;
                        order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                    }
                }

                _salesContext.PurchaseOrders.Remove(purchaseOrder);
                await _salesContext.PurchaseOrders.AddAsync(order, cancellationToken);
                await _salesContext.SaveChangesAsync(cancellationToken);

                return await Result<Guid>.SuccessAsync(purchaseOrder.Id, _localizer["purchase Order Deleted"]);
            }
            else
            {
                throw new SalesException(_localizer["purchase Order Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}

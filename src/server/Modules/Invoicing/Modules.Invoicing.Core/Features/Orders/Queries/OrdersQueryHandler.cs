using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    internal class OrdersQueryHandler :
                IRequestHandler<GetOrdersQuery, PaginatedResult<OrderResponseDto>>,
                IRequestHandler<GetOrderByIdQuery, Result<InternalOrderDto>>,
                IRequestHandler<GetFOByIdQuery, Result<OrderResponseDto>>,
                IRequestHandler<GetOrderForProcessQuery, List<InternalFulfillmentOrderDto>>,
                IRequestHandler<GetOrderForConfirmQuery, Result<InternalOrderDto>>,
                IRequestHandler<ScanLoadSheetOrderQuery, Result<InternalFulfillmentOrderDto>>

    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;
        private readonly IProductService _productService;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;
        private readonly IWarehouseService _warehouseService;

        public OrdersQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IProductService productService,
            IWarehouseService warehouseService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
            _warehouseService = warehouseService;
        }

        public async Task<PaginatedResult<OrderResponseDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderQueryable = _context.Orders.AsQueryable();
            var fulfillmentOrderQueryable = _context.FulfillmentOrders.AsQueryable();
            var addressesQueryable = _context.Addresses.AsQueryable();
            var warehousesQueryable = _context.Warehouses.AsQueryable();

            if (request.Status.HasValue)
            {
                fulfillmentOrderQueryable = fulfillmentOrderQueryable.Where(x => x.OrderStatus == request.Status.Value);
            }


            var orders = await (from o in orderQueryable
                                join fo in fulfillmentOrderQueryable on o.Id equals fo.InternalOrderId
                                join a in addressesQueryable on o.ShippingAddressId equals a.Id
                                join w in warehousesQueryable on fo.WarehouseId equals w.Id into ww
                                from m in ww.DefaultIfEmpty()
                                select new OrderResponseDto
                                {
                                    Id = o.Id,
                                    InternalFulFillmentOrderId = fo.Id,
                                    WarehouseId = m != null ? m.Id : null,
                                    Name = fo.Name,
                                    Address1 = a.Address1,
                                    Address2 = a.Address2,
                                    BranchId = o.BranchId,
                                    CancelledAt = o.CancelledAt,
                                    CancelReason = o.CancelReason,
                                    City = a.City,
                                    Country = a.Country,
                                    ClosedAt = o.ClosedAt,
                                    Confirmed = o.Confirmed,
                                    CreatedAt = o.CreatedAt,
                                    CurrentSubtotalPrice = o.CurrentSubtotalPrice,
                                    CurrentTotalDiscounts = o.CurrentTotalDiscounts,
                                    CurrentTotalPrice = o.CurrentTotalPrice,
                                    CurrentTotalTax = o.CurrentTotalTax,
                                    CustomerEmail = o.CustomerEmail,
                                    CustomerName = o.CustomerName,
                                    CustomerPhone = o.CustomerPhone,
                                    Email = o.Email,
                                    FirstName = a.FirstName,
                                    IpAddress = string.Empty,
                                    LastName = a.LastName,
                                    LocationId = o.LocationId,
                                    Note = o.Note,
                                    Number = o.Number,
                                    OrderNumber = o.OrderNumber,
                                    OrderType = o.OrderType,
                                    OrganizationId = o.OrganizationId,
                                    PaymentMethod = o.PaymentGatewayNames,
                                    Phone = o.Phone,
                                    ProcessedAt = o.ProcessedAt,
                                    Province = a.Province,
                                    ShopifyId = o.ShopifyId,
                                    Status = fo.OrderStatus,
                                    SubtotalPrice = fo.SubtotalPrice,
                                    TotalDiscounts = fo.TotalDiscounts,
                                    TotalPrice = fo.TotalPrice,
                                    TotalTax = fo.TotalTax,
                                    TaxesIncluded = o.TaxesIncluded,
                                    TimeStamp = o.TimeStamp,
                                    TotalLineItemsPrice = fo.TotalLineItemsPrice,
                                    TotalOutstanding = fo.TotalOutstanding,
                                    TotalShippingPrice = fo.TotalShippingPrice,
                                    TotalTipReceived = o.TotalTipReceived,
                                    TotalWeight = o.TotalWeight,
                                    UpdatedAt = o.UpdatedAt,
                                    WarehouseName = m != null ? m.Name : string.Empty,
                                    AssignedLocationId = fo.AssignedLocationId,
                                    FulFillmentOrderId = fo.ShopifyId,
                                    FulFillmentOrderStatus = fo.Status,
                                    TrackingCompany = fo.TrackingCompany,
                                    TrackingNumber = fo.TrackingNumber,
                                    TrackingStatus = fo.TrackingStatus
                                }).ToPaginatedListAsync(request.PageNumber, request.PageSize);


            //var queryable = _context.Orders.AsNoTracking()
            //    .Include(x => x.ShippingAddress)
            //    .AsQueryable();

            //// string ordering = new OrderByConverter().Convert(request.OrderBy);
            //// queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            //if (!string.IsNullOrEmpty(request.SearchString))
            //{
            //    queryable = queryable.Where(x => EF.Functions.Like(x.ReferenceNumber.ToLower(), $"%{request.SearchString.ToLower()}%")
            //    || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%") || x.Note.Contains(request.SearchString));
            //}

            //if (request.OrderType != null)
            //{
            //    queryable = queryable.Where(x => x.OrderType == request.OrderType);
            //}

            //if (request.Status != null)
            //{
            //    queryable = queryable.Where(x => x.Status == request.Status);
            //}

            //var saleList = await queryable
            //    .AsNoTracking()
            //    .OrderByDescending(x => x.CreatedAt)
            //    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            //if (saleList == null)
            //{
            //    throw new SalesException(_localizer["Sales Not Found!"], HttpStatusCode.NotFound);
            //}
            //var warehouseIds = saleList.Data.Select(x => x.WarehouseId).Distinct().ToList();
            //var warehouses = await _warehouseService.GetWarehouse(warehouseIds);
            //var mappedData = _mapper.Map<PaginatedResult<InternalOrderDto>>(saleList);

            //foreach (var item in mappedData.Data)
            //{
            //    var location = warehouses.Data.FirstOrDefault(x => x.Id == item.WarehouseId);
            //    if (location != null)
            //    {
            //        item.WarehouseName = location.Name;
            //    }
            //}

            return orders;
        }

        public async Task<Result<InternalOrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            InternalOrder order = null;
            var orderQueryable = _context.Orders
                .AsNoTracking()
                .Include(x => x.ShippingAddress)
                //.Include(x => x.BillingAddress)
                .Include(x => x.LineItems)
                .Include(x => x.Fulfillments)
                .Include(x => x.FulfillmentOrders)
                .AsQueryable();

            if (request.Id.HasValue)
            {
                order = await orderQueryable.FirstOrDefaultAsync(x => x.Id == request.Id);
            }
            else
            {
                order = await orderQueryable.FirstOrDefaultAsync(x => x.Name == request.OrderNo);
            }

            if (order == null)
            {
                throw new SalesException(_localizer["Order Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedData = _mapper.Map<InternalOrder, InternalOrderDto>(order);

            var ids = mappedData.LineItems.Select(x => x.ProductId).ToList();
            var productImages = await _productService.GetProductImages(ids);
            foreach (var item in mappedData.LineItems)
            {
                item.ImageUrl = productImages.Data.OrderBy(x => x.Position).FirstOrDefault(x => x.productId == item.ProductId)?.src;
            }

            var fulfillmentOrders = await _shopifyOrderFulFillmentService.GetFulFillOrderByOrderId(order.ShopifyId.Value);
            mappedData.FulfillmentOrders = _mapper.Map<List<InternalFulfillmentOrderDto>>(fulfillmentOrders);
            return await Result<InternalOrderDto>.SuccessAsync(data: mappedData);

        }

        public async Task<List<InternalFulfillmentOrderDto>> Handle(GetOrderForProcessQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.FulfillmentOrders.AsNoTracking()
                .Include(x => x.FulfillmentOrderDestination)
                .Include(x => x.FulfillmentOrderLineItems)
                .Where(x => x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.PendingApproval || x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.Pending || x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReQueueAfterReject)
                .ToListAsync();

            return _mapper.Map<List<InternalFulfillmentOrder>, List<InternalFulfillmentOrderDto>>(orders);
        }

        public async Task<Result<InternalOrderDto>> Handle(GetOrderForConfirmQuery request, CancellationToken cancellationToken)
        {
            string pattern = @"-S[0-9]+$";
            Regex regex = new Regex(pattern);
            bool isSplittedOrder = regex.IsMatch(request.OrderNo);

            InternalOrder order = null;
            InternalFulfillmentOrder fulfillmentOrder = null;

            if (isSplittedOrder)
            {
                var fulfillmentOrdersQueryable = _context.FulfillmentOrders
                                                        .AsNoTracking()
                                                        .AsQueryable();

                fulfillmentOrder = await fulfillmentOrdersQueryable.Include(x => x.FulfillmentOrderLineItems).FirstOrDefaultAsync(x => x.Name == request.OrderNo);

                if (fulfillmentOrder != null)
                {
                    order = await _context.Orders
                                   .AsNoTracking()
                                   .Include(x => x.LineItems)
                                   .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
                                   .FirstOrDefaultAsync(x => x.Id == fulfillmentOrder.InternalOrderId);
                }
            }
            else
            {
                order = await _context.Orders
                  .AsNoTracking()
                  .Include(x => x.LineItems)
                  .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
                  .FirstOrDefaultAsync(x => x.Name == request.OrderNo);

                if (order != null)
                {
                    fulfillmentOrder = order.FulfillmentOrders.FirstOrDefault();
                }
            }


            if (order == null)
            {
                throw new SalesException(_localizer["Order Not Found!"], HttpStatusCode.NotFound);
            }

            if (fulfillmentOrder.OrderStatus != Shared.DTOs.Sales.Enums.OrderStatus.Preparing)
            {
                if (fulfillmentOrder.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.AssignToOutlet)
                {
                    return await Result<InternalOrderDto>.ReturnErrorAsync(string.Format(_localizer["Pleae accept this order before confirm."], order.ShopifyId));
                }
                else if (fulfillmentOrder.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip)
                {
                    return await Result<InternalOrderDto>.ReturnErrorAsync(string.Format(_localizer["Order already confirmed."], order.ShopifyId));
                }
                else
                {
                    return await Result<InternalOrderDto>.ReturnErrorAsync(string.Format(_localizer["Order not found."], order.ShopifyId));
                }
            }

            var mappedData = _mapper.Map<InternalOrder, InternalOrderDto>(order);

            if (isSplittedOrder == true)
            {
                var foVariantIds = fulfillmentOrder.FulfillmentOrderLineItems.Select(x => x.VariantId).ToList();
                mappedData.LineItems = mappedData.LineItems.Where(x => foVariantIds.Contains(x.VariantId)).ToList();
            }

            mappedData.FulfillmentOrderId = fulfillmentOrder.ShopifyId;

            var ids = mappedData.LineItems.Select(x => x.ProductId).ToList();
            var productImages = await _productService.GetProductImages(ids);
            foreach (var item in mappedData.LineItems)
            {
                item.ImageUrl = productImages.Data.OrderBy(x => x.Position).FirstOrDefault(x => x.productId == item.ProductId)?.src;
            }

            return await Result<InternalOrderDto>.SuccessAsync(data: mappedData);
        }

        public async Task<Result<InternalFulfillmentOrderDto>> Handle(ScanLoadSheetOrderQuery request, CancellationToken cancellationToken)
        {
            bool isTrackingNumber = request.SearchText.All(char.IsDigit);
            InternalFulfillmentOrder fulfillmentOrder = null;
            var fulfillmentOrdersQueryable = _context.FulfillmentOrders
                                                        .AsNoTracking()
                                                        .Include(x => x.FulfillmentOrderDestination)
                                                        .Include(x => x.FulfillmentOrderLineItems)
                                                        .AsQueryable();

            if (isTrackingNumber)
            {
                fulfillmentOrder = await fulfillmentOrdersQueryable.FirstOrDefaultAsync(x => x.TrackingNumber == request.SearchText && x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip);
            }
            else
            {
                fulfillmentOrder = await fulfillmentOrdersQueryable.FirstOrDefaultAsync(x => x.Name == request.SearchText && x.OrderStatus == Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip);
            }

            if (fulfillmentOrder == null)
            {
                throw new SalesException(_localizer["Order Not Found! Please verify order confirmed or not."], HttpStatusCode.NotFound);
            }

            var mappedData = _mapper.Map<InternalFulfillmentOrder, InternalFulfillmentOrderDto>(fulfillmentOrder);

            return await Result<InternalFulfillmentOrderDto>.SuccessAsync(data: mappedData);
        }

        public async Task<Result<OrderResponseDto>> Handle(GetFOByIdQuery request, CancellationToken cancellationToken)
        {
            var orderQueryable = _context.Orders.AsQueryable();
            var fulfillmentOrderQueryable = _context.FulfillmentOrders.AsQueryable();
            var addressesQueryable = _context.Addresses.AsQueryable();
            var warehousesQueryable = _context.Warehouses.AsQueryable();

            var order = await (from o in orderQueryable
                               join fo in fulfillmentOrderQueryable on o.Id equals fo.InternalOrderId
                               join a in addressesQueryable on o.ShippingAddressId equals a.Id
                               join w in warehousesQueryable on fo.WarehouseId equals w.Id into ww
                               from m in ww.DefaultIfEmpty()
                               where fo.Id == request.Id
                               select new OrderResponseDto
                               {
                                   Id = o.Id,
                                   InternalFulFillmentOrderId = fo.Id,
                                   WarehouseId = m != null ? m.Id : null,
                                   Name = fo.Name,
                                   Address1 = a.Address1,
                                   Address2 = a.Address2,
                                   BranchId = o.BranchId,
                                   CancelledAt = o.CancelledAt,
                                   CancelReason = o.CancelReason,
                                   City = a.City,
                                   Country = a.Country,
                                   ClosedAt = o.ClosedAt,
                                   Confirmed = o.Confirmed,
                                   CreatedAt = o.CreatedAt,
                                   CurrentSubtotalPrice = o.CurrentSubtotalPrice,
                                   CurrentTotalDiscounts = o.CurrentTotalDiscounts,
                                   CurrentTotalPrice = o.CurrentTotalPrice,
                                   CurrentTotalTax = o.CurrentTotalTax,
                                   CustomerEmail = o.CustomerEmail,
                                   CustomerName = o.CustomerName,
                                   CustomerPhone = o.CustomerPhone,
                                   Email = o.Email,
                                   FirstName = a.FirstName,
                                   IpAddress = string.Empty,
                                   LastName = a.LastName,
                                   LocationId = o.LocationId,
                                   Note = o.Note,
                                   Number = o.Number,
                                   OrderNumber = o.OrderNumber,
                                   OrderType = o.OrderType,
                                   OrganizationId = o.OrganizationId,
                                   PaymentMethod = o.PaymentGatewayNames,
                                   Phone = o.Phone,
                                   ProcessedAt = o.ProcessedAt,
                                   Province = a.Province,
                                   ShopifyId = o.ShopifyId,
                                   Status = fo.OrderStatus,
                                   SubtotalPrice = fo.SubtotalPrice,
                                   TotalDiscounts = fo.TotalDiscounts,
                                   TotalPrice = fo.TotalPrice,
                                   TotalTax = fo.TotalTax,
                                   TaxesIncluded = o.TaxesIncluded,
                                   TimeStamp = o.TimeStamp,
                                   TotalLineItemsPrice = fo.TotalLineItemsPrice,
                                   TotalOutstanding = fo.TotalOutstanding,
                                   TotalShippingPrice = fo.TotalShippingPrice,
                                   TotalTipReceived = o.TotalTipReceived,
                                   TotalWeight = o.TotalWeight,
                                   UpdatedAt = o.UpdatedAt,
                                   WarehouseName = m != null ? m.Name : string.Empty,
                                   AssignedLocationId = fo.AssignedLocationId,
                                   FulFillmentOrderId = fo.ShopifyId,
                                   FulFillmentOrderStatus = fo.Status,
                                   TrackingCompany = fo.TrackingCompany,
                                   TrackingNumber = fo.TrackingNumber,
                                   TrackingStatus = fo.TrackingStatus,
                                   FulfillmentOrderLineItems = fo.FulfillmentOrderLineItems.Select(x => new InternalFulfillmentOrderLineItemDto
                                   {
                                       WarehouseId = x.WarehouseId,
                                       VariantId = x.VariantId,
                                       BranchId = x.BranchId,
                                       ConfirmedAt = x.ConfirmedAt,
                                       ConfirmedQty = x.ConfirmedQty,
                                       CreatedAt = x.CreatedAt,
                                       FulfillableQuantity = x.FulfillableQuantity,
                                       FulfillmentOrderId = x.FulfillmentOrderId,
                                       Id = x.Id,
                                       InventoryItemId = x.InventoryItemId,
                                       LineItemId = x.LineItemId,
                                       Quantity = x.Quantity,
                                       ShopifyId = x.ShopifyId,
                                   }).ToList(),
                                   LineItems = o.LineItems.Select(x => new OrderLineItemDto
                                   {
                                       ConfirmedAt = x.ConfirmedAt,
                                       ConfirmedQty = x.ConfirmedQty,
                                       CreatedAt = x.CreatedAt,
                                       FulfillableQuantity = x.FulfillableQuantity,
                                       FulfillmentLineItemId = x.FulfillmentLineItemId,
                                       FulfillmentService = x.FulfillmentService,
                                       FulfillmentStatus = x.FulfillmentStatus,
                                       GiftCard = x.GiftCard,
                                       Grams = x.Grams,
                                       Id = x.Id,
                                       ProductId = x.ProductId,
                                       Name = x.Name,
                                       PreTaxPrice = x.PreTaxPrice,
                                       Price = x.Price,
                                       ProductExists = x.ProductExists,
                                       Quantity = x.Quantity,
                                       RequiresShipping = x.RequiresShipping,
                                       ShopifyId = x.ShopifyId,
                                       SKU = x.SKU,
                                       Taxable = x.Taxable,
                                       Title = x.Title,
                                       TotalDiscount = x.TotalDiscount,
                                       VariantId = x.VariantId,
                                       VariantTitle = x.VariantTitle,
                                       WarehouseId = x.WarehouseId,
                                       Vendor = x.Vendor
                                   }).ToList()

                               }).FirstOrDefaultAsync();

            if (order == null)
            {
                throw new SalesException(_localizer["Order Not Found!"], HttpStatusCode.NotFound);
            }

            var ids = order.LineItems.Select(x => x.ProductId).ToList();
            var productImages = await _productService.GetProductImages(ids);
            foreach (var item in order.LineItems)
            {
                item.ImageUrl = productImages.Data.OrderBy(x => x.Position).FirstOrDefault(x => x.productId == item.ProductId)?.src;
            }

            return await Result<OrderResponseDto>.SuccessAsync(data: order);
        }

    }
}
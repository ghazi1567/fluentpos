using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    internal class OrdersQueryHandler :
                IRequestHandler<GetOrdersQuery, PaginatedResult<InternalOrderDto>>,
                IRequestHandler<GetOrderByIdQuery, Result<InternalOrderDto>>

    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;
        private readonly IProductService _productService;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;

        public OrdersQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
        }

        public async Task<PaginatedResult<InternalOrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Orders.AsNoTracking()
                .Include(x => x.ShippingAddress)
                .AsQueryable();

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            // queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.ReferenceNumber.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%") || x.Note.Contains(request.SearchString));
            }

            if (request.OrderType != null)
            {
                queryable = queryable.Where(x => x.OrderType == request.OrderType);
            }

            if (request.Status != null)
            {
                queryable = queryable.Where(x => x.Status == request.Status);
            }

            var saleList = await queryable
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (saleList == null)
            {
                throw new SalesException(_localizer["Sales Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<InternalOrderDto>>(saleList);

        }

        public async Task<Result<InternalOrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking()
                .Include(x => x.ShippingAddress)
                .Include(x => x.BillingAddress)
                .Include(x => x.LineItems)
                .Include(x => x.Fulfillments)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

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

            var fulfillmentOrders = await _shopifyOrderFulFillmentService.GetFulFillOrder(order.ShopifyId.Value);
            mappedData.FulfillmentOrders = _mapper.Map<List<InternalFulfillmentOrderDto>>(fulfillmentOrders);
            return await Result<InternalOrderDto>.SuccessAsync(data: mappedData);

        }
    }
}
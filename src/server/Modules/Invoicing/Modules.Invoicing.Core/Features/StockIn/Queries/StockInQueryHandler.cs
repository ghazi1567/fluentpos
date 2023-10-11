using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Invoicing.Core.Features.StockIn.Queries
{

    internal class StockInQueryHandler : IRequestHandler<GetStockInByIdQuery, Result<GetStockInByIdResponse>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StockInQueryHandler> _localizer;
        private readonly IProductService _productService;

        public StockInQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<StockInQueryHandler> localizer,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
        }

        public async Task<Result<GetStockInByIdResponse>> Handle(GetStockInByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking()
                .Include(x => x.Products)
                .OrderBy(x => x.TimeStamp)
                .SingleOrDefaultAsync(x => x.UUID == request.Id);

            if (order == null)
            {
                throw new SalesException(_localizer["Stock In Not Found!"], HttpStatusCode.NotFound);
            }

            var purchaseOrder = await _context.PurchaseOrders.AsNoTracking()
               .Include(x => x.Products)
               .OrderBy(x => x.TimeStamp)
               .SingleOrDefaultAsync(x => x.ReferenceNumber == order.POReferenceNo);

            if (purchaseOrder == null)
            {
                throw new SalesException(_localizer["Purchase Order Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedData = _mapper.Map<Order, GetStockInByIdResponse>(order);

            foreach (var item in mappedData.Products)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    item.productName = product.Name;
                    item.BarcodeSymbology = product.BarcodeSymbology;
                }

                var poItem = purchaseOrder.Products.SingleOrDefault(x => x.ProductId == item.ProductId);
                if (poItem != null)
                {
                    item.OrderedQuantity = poItem.Quantity;
                }
                else
                {
                    item.IsNotPO = true;
                }
            }

            return await Result<GetStockInByIdResponse>.SuccessAsync(data: mappedData);
        }
    }
}
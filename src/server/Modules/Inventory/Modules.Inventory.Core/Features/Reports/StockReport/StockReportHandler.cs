using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Reports
{
    internal class StockReportHandler : IRequestHandler<StockReportQuery, Result<List<StockDto>>>
    {
        private readonly IInventoryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StockReportHandler> _localizer;
        private readonly IProductService _productService;

        public StockReportHandler(
            IInventoryDbContext context,
            IMapper mapper,
            IStringLocalizer<StockReportHandler> localizer,
            IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
        }

        public async Task<Result<List<StockDto>>> Handle(StockReportQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Stocks.AsQueryable();
            var product = await _productService.GetProductBySKU(request.SKU);

            if (product != null)
            {
                query = query.Where(x => x.InventoryItemId == product.InventoryItemId);
            }

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                var productId = Guid.Parse(request.ProductId);
                query = query.Where(x => x.ProductId == productId);
            }

            if (!string.IsNullOrEmpty(request.WarehouseId))
            {
                var warehouseId = Guid.Parse(request.WarehouseId);
                query = query.Where(x => x.WarehouseId == warehouseId);
            }

            var result = query.ToList();

            return await Result<List<StockDto>>.SuccessAsync(data: _mapper.Map<List<StockDto>>(result));
        }
    }
}
using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
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
        private readonly IWarehouseService _warehouseService;

        public StockReportHandler(
            IInventoryDbContext context,
            IMapper mapper,
            IStringLocalizer<StockReportHandler> localizer,
            IProductService productService,
            IWarehouseService warehouseService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _productService = productService;
            _warehouseService = warehouseService;
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
            var stockListDto = _mapper.Map<List<StockDto>>(result);
            var productIds = stockListDto.Select(x => x.ProductId).Distinct().ToList();

            var paroducts = await _productService.GetProductByIds(productIds);
            var warehouses = await _warehouseService.GetWarehouse(new List<string>());
            foreach (var item in stockListDto)
            {
                var productVariant = paroducts.FirstOrDefault(x => x.InventoryItemId == item.InventoryItemId);
                if (productVariant != null)
                {
                    item.Title = productVariant.Title;
                    item.SKU = productVariant.SKU;
                }

                var warehouse = warehouses.Data.FirstOrDefault(x => x.Id == item.WarehouseId);
                if (warehouse != null)
                {
                    item.WarehouseName = warehouse.Name;
                }
            }

            return await Result<List<StockDto>>.SuccessAsync(data: stockListDto);
        }
    }
}
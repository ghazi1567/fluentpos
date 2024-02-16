using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Reports
{
    internal class StockReportHandler : IRequestHandler<StockReportQuery, PaginatedResult<StockDto>>
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

        public async Task<PaginatedResult<StockDto>> Handle(StockReportQuery request, CancellationToken cancellationToken)
        {

            var queryable = _context.Stocks.AsQueryable();

            if (request.WarehouseIds != null && request.WarehouseIds.Length > 0)
            {
                queryable = queryable.Where(x => request.WarehouseIds.Contains(x.WarehouseId));
            }

            if (request.AdvanceFilters != null && request.AdvanceFilters.Count > 0)
            {
                queryable = queryable.AdvanceSearch(request.AdvanceFilters, request.AdvancedSearchType);
            }

            if (request.SortModel != null && request.SortModel.Count > 0)
            {
                queryable = queryable.AdvanceSort(request.SortModel);
            }

            var result = await queryable.AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var stockListDto = _mapper.Map<PaginatedResult<StockDto>>(result);
            var productIds = stockListDto.Data.Select(x => x.ProductId).Distinct().ToList();

            var paroducts = await _productService.GetProductByIds(productIds);
            var warehouses = await _warehouseService.GetWarehouse(new List<string>());
            foreach (var item in stockListDto.Data)
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

            return stockListDto;
        }
    }
}
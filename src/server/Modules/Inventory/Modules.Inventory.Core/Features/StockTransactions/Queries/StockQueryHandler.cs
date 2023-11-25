using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Features.Queries;
using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class StockQueryHandler : IRequestHandler<GetStockBySKUs, List<WarehouseStockStatsDto>>,
         IRequestHandler<GetStockByVariantIds, List<WarehouseStockStatsDto>>
    {
        private readonly IStringLocalizer<StockQueryHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IInventoryDbContext _context;

        public StockQueryHandler(
            IStringLocalizer<StockQueryHandler> localizer,
            IMapper mapper,
            IInventoryDbContext context)
        {
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<WarehouseStockStatsDto>> Handle(GetStockBySKUs request, CancellationToken cancellationToken)
        {
            var result = await _context.Stocks.AsNoTracking()
                .Where(x => request.SKUs.Contains(x.SKU))
                .ToListAsync();

            return result.Select(x => new WarehouseStockStatsDto
            {
                Distance = 0,
                inventoryItemId = x.InventoryItemId,
                Latitude = 0,
                Longitude = 0,
                Name = "",
                productId = x.ProductId,
                quantity = x.AvailableQuantity,
                Rack = x.Rack,
                SKU = x.SKU,
                VariantId = x.VariantId,
                warehouseId = x.WarehouseId,

            }).ToList();
        }

        public async Task<List<WarehouseStockStatsDto>> Handle(GetStockByVariantIds request, CancellationToken cancellationToken)
        {
            var result = await _context.Stocks.AsNoTracking()
                .Where(x => request.VariantIds.Contains(x.InventoryItemId))
                .ToListAsync();

            return result.Select(x => new WarehouseStockStatsDto
            {
                Distance = 0,
                inventoryItemId = x.InventoryItemId,
                Latitude = 0,
                Longitude = 0,
                Name = "",
                productId = x.ProductId,
                quantity = x.AvailableQuantity,
                Rack = x.Rack,
                SKU = x.SKU,
                VariantId = x.VariantId,
                warehouseId = x.WarehouseId,
                Id = x.Id,
                BranchId = x.BranchId,
                OrganizationId = x.OrganizationId,

            }).ToList();
        }
    }
}
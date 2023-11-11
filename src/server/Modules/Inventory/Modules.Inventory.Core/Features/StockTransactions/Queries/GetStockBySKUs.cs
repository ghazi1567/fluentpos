using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Features.Queries
{
    public class GetStockBySKUs : IRequest<List<WarehouseStockStatsDto>>
    {
        public List<string> SKUs { get; set; }

        public GetStockBySKUs(List<string> sku)
        {
            SKUs = sku;
        }
    }
}
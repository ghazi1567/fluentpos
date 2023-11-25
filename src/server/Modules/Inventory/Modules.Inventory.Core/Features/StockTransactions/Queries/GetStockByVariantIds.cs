using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Features.Queries
{
    public class GetStockByVariantIds : IRequest<List<WarehouseStockStatsDto>>
    {
        public List<long> VariantIds { get; set; }

        public GetStockByVariantIds(List<long> variantIds)
        {
            VariantIds = variantIds;
        }
    }
}
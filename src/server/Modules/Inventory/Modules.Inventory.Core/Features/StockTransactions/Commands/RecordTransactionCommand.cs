using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class RecordTransactionCommand : IRequest<Result<Guid>>
    {
        public Guid productId { get; set; }

        public long inventoryItemId { get; set; }

        public long quantity { get; set; }

        public OrderType type { get; set; }

        public Guid warehouseId { get; set; }

        public string Rack { get; set; }

        public bool IgnoreRackCheck { get; set; }
    }
}
using FluentPOS.Shared.DTOs.Inventory;
using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{

    public class SplitOrderResult
    {
        public long? InternalOrderId { get; set; }

        public long? FulfillmentOrderId { get; set; }

        public long? FOShopifyId { get; set; }
        public long? AssignedLocationId { get; set; }

        /// <summary>
        /// Is single store able to fill order.
        /// </summary>
        public bool IsSingleStore { get; set; }

        /// <summary>
        /// Number of fulfillment order in which an order will be splitted.
        /// </summary>
        public int SplitCount { get; set; }

        /// <summary>
        /// Ids of warehouse that will fulfill orders.
        /// </summary>
        public List<long> WarehouseIds { get; set; }

        public List<SplitOrderDetailDto> SplitOrderDetails { get; set; }

        public List<WarehouseStockStatsDto> WarehouseStocks { get; set; }


    }

    public class SplitOrderDetailDto
    {
        public long? WarehouseId { get; set; }

        public long InventoryItemId { get; set; }

        public long RequiredQuantity { get; set; }

        public WarehouseStockStatsDto? Warehouse { get; set; }

        public long AvailableQuantity { get; set; }

        public long FulfillableQuantity { get; set; }

        public bool CanFulFill { get; set; }

        public int CanFulfillCount { get; set; }

        public decimal Distance { get; set; }

        public List<long> FulfillableIds { get; set; }

        public long LineItemId { get; set; }

    }
}
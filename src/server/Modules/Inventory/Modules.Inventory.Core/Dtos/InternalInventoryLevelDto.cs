using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class InternalInventoryLevelDto : BaseEntityDto
    {
        /// <summary>
        /// The unique identifier of the inventory item that the inventory level belongs to.
        /// </summary>
        public long? InventoryItemId { get; set; }

        /// <summary>
        /// The unique identifier of the location that the inventory level belongs to.
        /// </summary>
        public long? LocationId { get; set; }

        /// <summary>
        /// The quantity of inventory items available for sale. Returns null if the inventory item is not tracked.
        /// </summary>
        public long? Available { get; set; }

        public long? WarehouseId { get; set; }

        public long? ParentId { get; set; }
    }
}
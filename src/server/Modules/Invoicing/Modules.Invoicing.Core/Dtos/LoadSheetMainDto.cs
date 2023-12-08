using FluentPOS.Shared.DTOs.Dtos;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class LoadSheetMainDto : BaseEntityDto
    {
        public long TotalOrder { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public string ContactNumber { get; set; }

        public string PickupAddress { get; set; }

        public string CityName { get; set; }

        public long WarehouseId { get; set; }

        public IEnumerable<LoadSheetDetailDto> Details { get; set; }
    }
}
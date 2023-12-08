using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("LoadSheetMains")]
    public class LoadSheetMain : BaseEntity
    {
        public long TotalOrder { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public string ContactNumber { get; set; }

        public string PickupAddress { get; set; }

        public string CityName { get; set; }

        public long WarehouseId { get; set; }

        public IEnumerable<LoadSheetDetail> Details { get; set; }
    }
}
using FluentPOS.Shared.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("OrderLogs")]
    public class OrderLogs : BaseEntity
    {
        public long InternalOrderId { get; set; }

        public long? FulfillmentOrderId { get; set; }

        public string LogDescription { get; set; }

        public long WarehouseId { get; set; }

        public bool Ignore { get; set; }
    }
}
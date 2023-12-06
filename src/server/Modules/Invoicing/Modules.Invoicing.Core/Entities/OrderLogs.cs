using FluentPOS.Shared.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("OrderLogs")]
    public class OrderLogs : BaseEntity
    {
        public Guid InternalOrderId { get; set; }

        public Guid? FulfillmentOrderId { get; set; }

        public string LogDescription { get; set; }
    }
}
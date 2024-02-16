using FluentPOS.Shared.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("LoadSheetDetails")]
    public class LoadSheetDetail : BaseEntity
    {
        public string OrderNumber { get; set; }

        public string TrackingNumber { get; set; }

        public string City { get; set; }

        public string PaymentMethod { get; set; }

        public long TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }

        public long InternalFulFillmentOrderId { get; set; }

        public long FulFillmentOrderId { get; set; }

        public long InternalOrderId { get; set; }

        public long OrderId { get; set; }

    }
}

using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class InvoiceDetail : BaseEntity
    {
        public long InvoiceId { get; set; }

        public string OrderNo { get; set; }

        public string TrackingNumber { get; set; }

        public string Weight { get; set; }

        public DateTime PickupDate { get; set; }

        public string OriginCity { get; set; }

        public string DeliveryCity { get; set; }

        public string Status { get; set; }

        public decimal CODAmount { get; set; }

        public decimal UpfrontAmount { get; set; }

        public decimal ReserveAmount { get; set; }

        public DateTime DeliverDate { get; set; }

        public decimal ShippingCharges { get; set; }

        public decimal UpfrontCharges { get; set; }

        public decimal NetAmount { get; set; }

        public decimal Tax { get; set; }

        public decimal NetAmountReceivable { get; set; }

        public bool IsValid { get; set; }

        public long FulfillmentOrderId { get; set; }
    }
}

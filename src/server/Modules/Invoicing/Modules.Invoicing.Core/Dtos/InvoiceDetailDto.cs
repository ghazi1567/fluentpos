using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InvoiceDetailDto : BaseEntityDto
    {
        public long InvoiceId { get; set; }

        public string OrderNo { get; set; }

        public string TrackingNumber { get; set; }

        public string Weight { get; set; }

        public DateTime PickupDate { get; set; }

        public string OriginCity { get; set; }

        public string DeliveryCity { get; set; }

        public string Status { get; set; }

        public double CODAmount { get; set; }

        public double UpfrontAmount { get; set; }

        public double ReserveAmount { get; set; }

        public DateTime DeliverDate { get; set; }

        public double ShippingCharges { get; set; }

        public double UpfrontCharges { get; set; }

        public double NetAmount { get; set; }

        public double Tax { get; set; }

        public double NetAmountReceivable { get; set; }

        public bool IsValid { get; set; }
    }
}

using FluentPOS.Shared.DTOs.Dtos;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class LoadSheetDetailDto : BaseEntityDto
    {
        public string OrderNumber { get; set; }

        public string TrackingNumber { get; set; }

        public string City { get; set; }

        public string PaymentMethod { get; set; }

        public long TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }

        public long FulfillmentOrderId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsNew { get; set; }

    }
}

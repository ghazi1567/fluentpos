using System;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class CityCorrectionOrderDto
    {
        public string OrderNo { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string CorrectCity { get; set; }

        public Guid Id { get; set; }

        public Guid FulfillmentOrderId { get; set; }
    }
}

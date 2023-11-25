namespace FluentPOS.Shared.DTOs.Dtos.Logistics
{
    public class PostexOrderModel
    {
        public string CityName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string DeliveryAddress { get; set; }

        public int InvoiceDivision { get; set; }

        public decimal InvoicePayment { get; set; }

        public int Items { get; set; }

        public string OrderDetail { get; set; }

        public string OrderRefNumber { get; set; }

        public string OrderType { get; set; }

        public string PickupAddressCode { get; set; }
    }
}

using System;

namespace FluentPOS.Shared.DTOs.Dtos.Logistics
{
    public class PostexResponseModel
    {
        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public PostexTrackingDetail Dist { get; set; }
    }

    public class PostexTrackingDetail
    {
        public string TrackingNumber { get; set; }

        public string OrderStatus { get; set; }

        public string OrderDate { get; set; }
    }
}
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Dtos.Logistics
{
    public class PostexLoadSheetModel
    {
        public List<string> TrackingNumbers { get; set; }

        public string CityName { get; set; }

        public string PickupAddress { get; set; }

        public string ContactNumber { get; set; }

    }
}

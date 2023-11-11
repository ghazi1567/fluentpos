using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class OperationCity : BaseEntity
    {
        public string CityName { get; set; }

        public string CountryName { get; set; }

        public bool CanPickup { get; set; }

        public bool CanDeliver { get; set; }
    }
}

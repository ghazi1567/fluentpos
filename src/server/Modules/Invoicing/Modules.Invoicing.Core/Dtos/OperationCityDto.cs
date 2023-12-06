using FluentPOS.Shared.DTOs.Dtos;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class OperationCityDto : BaseEntityDto
    {
        public string CityName { get; set; }

        public string CountryName { get; set; }

        public bool CanPickup { get; set; }

        public bool CanDeliver { get; set; }
    }
}

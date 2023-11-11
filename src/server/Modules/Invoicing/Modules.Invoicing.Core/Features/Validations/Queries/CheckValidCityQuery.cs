using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class CheckValidCityQuery : IRequest<bool>
    {
        public string CityName { get; set; }
        public CheckValidCityQuery(string cityName)
        {
            CityName = cityName;
        }

    }
}
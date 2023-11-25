using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Dtos.GeoLocation;
using FluentPOS.Shared.DTOs.Sales.Orders;
using GoogleMaps.LocationServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IMediator _mediator;

        public WarehouseService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<string> names)
        {
            return await _mediator.Send(new GetWarehouseByNamesQuery(names));
        }

        public MapPoint GetLatLongFromAdrress(string address)
        {
            var gls = new GoogleLocationService("AIzaSyBoDz4SW5eUkBl6jSJ-TEy51WFPt9QhPuo");
            var point = gls.GetLatLongFromAddress(address);
            return point;
        }

        public async Task<LocationResult> GetLatLongFromAdrressAsync(string address1, string address2, string city, string country)
        {
            string address = $"{address1}";
            if (!string.IsNullOrEmpty(address2))
            {
                address = $"{address}, {address2}";
            }

            address = $"{address}, {city}, {country}";

            LocationResult result = null;
            using (var client = new HttpClient())
            {
                // https://us1.locationiq.com/v1/search?key=YOUR_API_KEY&q=Statue%20of%20Liberty,%20New%20York&format=json
                client.BaseAddress = new Uri("https://us1.locationiq.com");
                var url = "/v1/search?key=pk.45a297a68ae69c4082a822ef5f76ed64&q=" + address + "&format=json";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseModel = await response.Content.ReadFromJsonAsync<List<LocationResult>>();
                    result = responseModel.OrderByDescending(x => x.importance).FirstOrDefault();
                }
                else
                {
                    var responseModel = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }

        public async Task<GeoLocation> GetLatLongFromAdrressAsync(string address1, string address2, string city, string zipCode, string country)
        {
            string address = $"{address1}";
            if (!string.IsNullOrEmpty(address2))
            {
                address = $"{address}, {address2}";
            }

            address = $"{address}, {city}, {country}";

            if (!string.IsNullOrEmpty(zipCode))
            {
                address = $"{address}, {zipCode}";
            }

            address = $"{address}, {country}";

            GeocodingResult result = null;
            using (var client = new HttpClient())
            {
                // https://us1.locationiq.com/v1/search?key=YOUR_API_KEY&q=Statue%20of%20Liberty,%20New%20York&format=json
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                var url = "/maps/api/geocode/json?address=" + address + "&key=AIzaSyCPGJBYISa6Yuub-WNNYPY6ZYV8cAqWk18";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    var responseModel = await response.Content.ReadFromJsonAsync<GeocodingLocationResult>();
                    result = responseModel.results.FirstOrDefault();
                }
                else
                {
                    var responseModel = await response.Content.ReadAsStringAsync();
                }
            }

            GeoLocation geoLocation = null;
            if (result != null)
            {
                geoLocation = result.Geometry.Location;
            }

            return geoLocation;
        }

        public async Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids)
        {
            return await _mediator.Send(new GetWarehouseByIdsQuery(ids));
        }
    }
}
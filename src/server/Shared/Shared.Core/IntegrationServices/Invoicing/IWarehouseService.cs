using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Dtos.GeoLocation;
using FluentPOS.Shared.DTOs.Sales.Orders;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Invoicing
{
    public interface IWarehouseService
    {
        Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<string> names);

        Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids);

        Task<LocationResult> GetLatLongFromAdrressAsync(string address1, string address2, string city, string country);

        Task<GeoLocation> GetLatLongFromAdrressAsync(string address1, string address2, string city, string zipCode, string country);
    }
}

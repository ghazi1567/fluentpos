using FluentPOS.Shared.Core.Wrapper;
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

        MapPoint GetLatLongFromAdrress(string address);
    }
}

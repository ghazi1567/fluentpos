using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public interface ILookupService
    {
        Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids);
    }

    public class LookupService : ILookupService
    {
        private readonly IWarehouseService _warehouseService;

        public LookupService(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public async Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids)
        {
            return await _warehouseService.GetWarehouse(ids);
        }
    }
}
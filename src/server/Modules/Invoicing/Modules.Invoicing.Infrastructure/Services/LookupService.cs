using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public interface ILookupService
    {
        Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids);

        Task<List<OperationCityDto>> GetOperationalCity();
    }

    public class LookupService : ILookupService
    {
        private readonly IWarehouseService _warehouseService;

        private readonly ISalesDbContext _context;

        public LookupService(
            IWarehouseService warehouseService,
            ISalesDbContext salesDbContext)
        {
            _warehouseService = warehouseService;
            _context = salesDbContext;
        }

        public async Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<Guid> ids)
        {
            return await _warehouseService.GetWarehouse(ids);
        }

        public async Task<List<OperationCityDto>> GetOperationalCity()
        {
            return await _context.OperationCity.OrderBy(x=>x.CityName).Select(x => new OperationCityDto
            {
                BranchId = x.BranchId,
                CanDeliver = x.CanDeliver,
                CanPickup = x.CanPickup,
                CityName = x.CityName,
                CountryName = x.CountryName,
                CreatedAt = x.CreatedAt,
                Id = x.Id,
                OrganizationId = x.OrganizationId,
                ShopifyId = x.ShopifyId,
                UpdatedAt = x.UpdatedAt,
            }).ToListAsync();
        }
    }
}
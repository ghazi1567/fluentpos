using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Invoicing
{
    public interface IWarehouseService
    {
        Task<Result<List<GetWarehouseResponse>>> GetWarehouse(List<string> names);
    }
}

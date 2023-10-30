using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using System.Collections.Generic;
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
    }
}
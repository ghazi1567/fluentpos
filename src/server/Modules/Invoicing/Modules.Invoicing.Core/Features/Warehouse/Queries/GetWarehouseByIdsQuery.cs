using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetWarehouseByNamesQuery : IRequest<Result<List<GetWarehouseResponse>>>
    {
        public List<string> Names { get; set; }

        public GetWarehouseByNamesQuery(List<string> name)
        {
            Names = name;
        }
    }
}
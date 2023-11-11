using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetWarehouseByIdsQuery : IRequest<Result<List<GetWarehouseResponse>>>
    {
        public List<Guid> Ids { get; set; }

        public GetWarehouseByIdsQuery(List<Guid> ids)
        {
            Ids = ids;
        }
    }
}
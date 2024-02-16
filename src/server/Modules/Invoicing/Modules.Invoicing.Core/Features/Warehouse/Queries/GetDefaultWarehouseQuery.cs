using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetWarehouseQuery : CacheableFilter<WarehouseDto>, IRequest<Result<List<WarehouseDto>>>
    {
        public long Id { get; set; }

        public long[] WarehouseIds { get; set; }

    }
}
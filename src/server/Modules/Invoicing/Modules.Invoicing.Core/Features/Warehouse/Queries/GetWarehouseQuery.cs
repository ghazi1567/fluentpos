using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Features.Common.Filters;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetDefaultWarehouseQuery : CacheableFilter<WarehouseDto>, IRequest<WarehouseDto>
    {
    }
}
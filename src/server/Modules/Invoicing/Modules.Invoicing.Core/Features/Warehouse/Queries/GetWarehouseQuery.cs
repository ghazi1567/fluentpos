using FluentPOS.Modules.Invoicing.Core.Dtos;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetDefaultWarehouseQuery : IRequest<WarehouseDto>
    {
    }
}
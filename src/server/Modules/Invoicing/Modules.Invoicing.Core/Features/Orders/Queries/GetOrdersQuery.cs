using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetOrdersQuery : IRequest<PaginatedResult<OrderResponseDto>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string[] OrderBy { get; set; }

        public string SearchString { get; set; }

        public OrderStatus? Status { get; set; }

        public OrderType? OrderType { get; set; }

        public long[] WarehouseIds { get; set; } = new long[0];
    }
}
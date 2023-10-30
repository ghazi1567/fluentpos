using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<Result<GetOrderByIdResponse>>
    {
        public Guid Id { get; set; }

    }
}
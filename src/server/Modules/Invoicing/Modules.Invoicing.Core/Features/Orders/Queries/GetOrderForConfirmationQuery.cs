using FluentPOS.Modules.Invoicing.Core.Dtos;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetOrderForConfirmationQuery : IRequest<List<InternalFulfillmentOrderDto>>
    {
    }
}
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetInvoicesQuery : IRequest<Result<List<InvoiceDto>>>
    {

    }
}
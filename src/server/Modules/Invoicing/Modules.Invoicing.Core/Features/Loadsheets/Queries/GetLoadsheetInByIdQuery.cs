using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries
{
    public class GetLoadsheetInByIdQuery : IRequest<Result<LoadSheetMainDto>>
    {
        public Guid Id { get; set; }

    }
}
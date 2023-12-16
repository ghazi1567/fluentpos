using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Features.Queries
{
    public class GetInventoryForProcessQuery : IRequest<List<ImportRecordDto>>
    {
    }
}
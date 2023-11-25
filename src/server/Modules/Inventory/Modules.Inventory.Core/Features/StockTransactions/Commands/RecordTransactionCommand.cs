using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class RecordTransactionCommand : StockTransactionDto, IRequest<Result<Guid>>
    {
        
    }
}
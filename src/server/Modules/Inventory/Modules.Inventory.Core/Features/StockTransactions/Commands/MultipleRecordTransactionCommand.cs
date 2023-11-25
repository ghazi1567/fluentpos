using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class MultipleRecordTransactionCommand : IRequest<Result<Guid>>
    {
        public List<StockTransactionDto> Transactions { get; set; }

        public MultipleRecordTransactionCommand(List<StockTransactionDto> stockTransactionDtos)
        {
            Transactions = stockTransactionDtos;
        }
    }
}
using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class StockTransactionCommandHandler : IRequestHandler<RecordTransactionCommand, Result<Guid>>
    {
        private readonly IStringLocalizer<StockTransactionCommandHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IInventoryDbContext _context;

        public StockTransactionCommandHandler(
            IStringLocalizer<StockTransactionCommandHandler> localizer,
            IMapper mapper,
            IInventoryDbContext context)
        {
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Guid>> Handle(RecordTransactionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IgnoreRackCheck)
            {
                var existingRecord = _context.Stocks.AsNoTracking().Where(s => s.ProductId == request.productId && s.InventoryItemId == request.inventoryItemId && s.WarehouseId == request.warehouseId).ToList();
                if (existingRecord.Count > 0)
                {
                    bool currentRackExists = existingRecord.Exists(x => x.Rack == request.Rack);

                    // current rack not exist but other does
                    if (currentRackExists == false)
                    {
                        return await Result<Guid>.FailAsync(string.Format(_localizer["Exists in other rack."]));
                    }
                }
            }

            var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == request.productId && s.InventoryItemId == request.inventoryItemId && s.WarehouseId == request.warehouseId && s.Rack == request.Rack);
            var stockTransaction = new StockTransaction(request.productId, request.inventoryItemId, request.quantity, request.type, request.warehouseId);
            await _context.StockTransactions.AddAsync(stockTransaction);

            bool isRecordExist = true;
            if (stockRecord == null)
            {
                stockRecord = new Stock(request.productId, request.inventoryItemId, request.warehouseId);
                stockRecord.Rack = request.Rack;
                stockRecord.SKU = request.SKU;
                stockRecord.VariantId = request.VariantId;
                isRecordExist = false;
            }

            switch (request.type)
            {
                case OrderType.FulFill:
                    stockRecord.FulFillCommittedQuantity(request.quantity);
                    break;
                case OrderType.StockIn:
                case OrderType.StockReturn:
                    stockRecord.IncreaseQuantity(request.quantity);
                    break;
                case OrderType.StockUpdate:
                    stockRecord.UpdateQuantity(request.quantity);
                    break;
                case OrderType.Commited:
                    stockRecord.IncreaseCommittedQuantity(request.quantity);
                    break;

                case OrderType.uncommitted:
                    stockRecord.ReduceCommittedQuantity(request.quantity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request.type), request.type, null);
            }

            if (isRecordExist)
            {
                _context.Stocks.Update(stockRecord);
            }
            else
            {
                _context.Stocks.Add(stockRecord);
            }

            await _context.SaveChangesAsync();

            return await Result<Guid>.SuccessAsync(stockRecord.Id, string.Format(_localizer["{0} Inventory Stock Transaction Updated."], request.inventoryItemId));
        }
    }
}
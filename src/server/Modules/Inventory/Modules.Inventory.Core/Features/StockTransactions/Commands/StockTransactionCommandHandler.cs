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
    public class StockTransactionCommandHandler : IRequestHandler<RecordTransactionCommand, Result<long>>,
        IRequestHandler<MultipleRecordTransactionCommand, Result<long>>
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

        public async Task<Result<long>> Handle(RecordTransactionCommand request, CancellationToken cancellationToken)
        {
            var response = await _context.ExecuteProcedureAsync<Result>("SP_RECORD_STOCK_TRANSACTION", new
            {
                IgnoreRackCheck = 0,
                InventoryItemId = request.inventoryItemId,
                ProductId = request.productId,
                Quantity = request.quantity,
                Rack = request.Rack,
                Type = request.type,
                WarehouseId = request.warehouseId,
                SKU = request.SKU,
                VariantId = request.VariantId,
                RecordId = request.ImportRecordId
            });
            var result = response.FirstOrDefault();
            if (result.Succeeded)
            {
                return await Result<long>.SuccessAsync(result.Message);
            }
            else
            {
                return await Result<long>.FailAsync(result.Message);
            }


            //if (!request.IgnoreRackCheck)
            //{
            //    var existingRecord = _context.Stocks.AsNoTracking().Where(s => s.ProductId == request.productId && s.InventoryItemId == request.inventoryItemId && s.WarehouseId == request.warehouseId).ToList();
            //    if (existingRecord.Count > 0)
            //    {
            //        bool currentRackExists = existingRecord.Exists(x => x.Rack == request.Rack);

            //        // current rack not exist but other does
            //        if (currentRackExists == false)
            //        {
            //            return await Result<long>.FailAsync(string.Format(_localizer["Exists in other rack."]));
            //        }
            //    }
            //}

            //var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == request.productId && s.InventoryItemId == request.inventoryItemId && s.WarehouseId == request.warehouseId && s.Rack == request.Rack);
            //var stockTransaction = new StockTransaction(request.productId, request.inventoryItemId, request.quantity, request.type, request.warehouseId);
            //await _context.StockTransactions.AddAsync(stockTransaction);

            //bool isRecordExist = true;
            //if (stockRecord == null)
            //{
            //    stockRecord = new Stock(request.productId, request.inventoryItemId, request.warehouseId);
            //    stockRecord.Rack = request.Rack;
            //    stockRecord.SKU = request.SKU;
            //    stockRecord.VariantId = request.VariantId;
            //    isRecordExist = false;
            //}

            //switch (request.type)
            //{
            //    case OrderType.FulFill:
            //        stockRecord.FulFillCommittedQuantity(request.quantity);
            //        break;
            //    case OrderType.StockIn:
            //    case OrderType.StockReturn:
            //        stockRecord.IncreaseQuantity(request.quantity);
            //        break;
            //    case OrderType.StockUpdate:
            //        stockRecord.UpdateQuantity(request.quantity);
            //        break;
            //    case OrderType.Commited:
            //        stockRecord.IncreaseCommittedQuantity(request.quantity);
            //        break;

            //    case OrderType.uncommitted:
            //        stockRecord.ReduceCommittedQuantity(request.quantity);
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(request.type), request.type, null);
            //}

            //if (isRecordExist)
            //{
            //    _context.Stocks.Update(stockRecord);
            //}
            //else
            //{
            //    _context.Stocks.Add(stockRecord);
            //}

            //await _context.SaveChangesAsync();

            //return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["{0} Inventory Stock Transaction Updated."], request.inventoryItemId));
        }

        public async Task<Result<long>> Handle(MultipleRecordTransactionCommand command, CancellationToken cancellationToken)
        {
            foreach (var request in command.Transactions)
            {
                var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == request.productId && s.InventoryItemId == request.inventoryItemId && s.WarehouseId == request.warehouseId);
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
            }

            //await _context.SaveChangesAsync();
            return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["{0} Inventory Stock Transaction Updated."], command.Transactions.Count));
        }
    }
}
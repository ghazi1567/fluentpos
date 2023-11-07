// --------------------------------------------------------------------------------------------------
// <copyright file="StockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    /// <inheritdoc/>
    public class StockService : IStockService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StockService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> RecordTransaction(StockTransactionDto stockTransactionDto)
        {
            var command = _mapper.Map<RecordTransactionCommand>(stockTransactionDto);
            return await _mediator.Send(command);
        }

        // public async Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType transactionType, decimal discountFactor, decimal purchasePrice, DateTime factorDate, Guid WarehouseId)
        // {
        //     // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.
        //     var stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber, discountFactor, purchasePrice, factorDate);
        //     stockTransaction.WarehouseId = WarehouseId;
        //     await _context.StockTransactions.AddAsync(stockTransaction);
        //     var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId);
        //     switch (transactionType)
        //     {
        //         case OrderType.StockOut:
        //             if (stockRecord != null)
        //             {
        //                 stockRecord.ReduceQuantity(quantity);
        //                 _context.Stocks.Update(stockRecord);
        //             }
        //             else
        //             {
        //                 stockRecord = new Stock(productId, WarehouseId);
        //                 stockRecord.ReduceQuantity(quantity);
        //                 _context.Stocks.Add(stockRecord);
        //             }
        //             break;
        //         case OrderType.StockIn:
        //         case OrderType.StockReturn:
        //             if (stockRecord != null)
        //             {
        //                 stockRecord.IncreaseQuantity(quantity);
        //                 _context.Stocks.Update(stockRecord);
        //             }
        //             else
        //             {
        //                 stockRecord = new Stock(productId, WarehouseId);
        //                 stockRecord.IncreaseQuantity(quantity);
        //                 _context.Stocks.Add(stockRecord);
        //             }
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
        //     }
        //     await _context.SaveChangesAsync();
        // }
        // public async Task ReverseTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType orderType, Guid WarehouseId)
        // {
        //     // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.
        //     var stockTransaction = _context.StockTransactions.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId && s.ReferenceNumber == referenceNumber && s.Type == orderType);
        //     if (stockTransaction != null)
        //     {
        //         _context.StockTransactions.Remove(stockTransaction);
        //     }
        //     var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId);
        //     switch (orderType)
        //     {
        //         case OrderType.StockOut:
        //             if (stockRecord != null)
        //             {
        //                 stockRecord.IncreaseQuantity(quantity);
        //                 _context.Stocks.Update(stockRecord);
        //             }
        //             else
        //             {
        //                 stockRecord = new Stock(productId, WarehouseId);
        //                 stockRecord.IncreaseQuantity(quantity);
        //                 _context.Stocks.Add(stockRecord);
        //             }
        //             break;
        //         case OrderType.StockIn:
        //         case OrderType.StockReturn:
        //             if (stockRecord != null)
        //             {
        //                 stockRecord.ReduceQuantity(quantity);
        //                 _context.Stocks.Update(stockRecord);
        //             }
        //             else
        //             {
        //                 stockRecord = new Stock(productId, WarehouseId);
        //                 stockRecord.ReduceQuantity(quantity);
        //                 _context.Stocks.Add(stockRecord);
        //             }
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(orderType), orderType, null);
        //     }
        //     await _context.SaveChangesAsync();
        // }
        // public async Task UpdateFactor(List<ProductFactorDto> productFactorDtos, DateTime updateFrom)
        // {
        //     var productIds = productFactorDtos.Select(x => x.ProductId).Distinct().ToList();
        //     var stockRecord = _context.StockTransactions.Where(s => s.Timestamp >= updateFrom && productIds.Contains(s.ProductId)).ToList();
        //     foreach (var item in stockRecord)
        //     {
        //         var product = productFactorDtos.SingleOrDefault(x => x.ProductId == item.ProductId);
        //         if (product != null)
        //         {
        //             item.FactorDate = product.FactorDate;
        //             item.DiscountFactor = product.FactorAmount;
        //         }
        //     }
        //     _context.StockTransactions.UpdateRange(stockRecord);
        //     await _context.SaveChangesAsync();
        // }
        // public async Task RecordOpeningTransaction(Guid productId, decimal quantity, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate, Guid WarehouseId)
        // {
        //     // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.
        //     OrderType transactionType = OrderType.OpeningStock;
        //     decimal lastQuantity = 0;
        //     Guid lastWarehouse = Guid.Empty;
        //     bool isAlreadyExist = false;
        //     bool isWarehouseChanged = false;
        //     var stockTransaction = _context.StockTransactions.FirstOrDefault(s => s.ProductId == productId && s.Type == transactionType);
        //     if (stockTransaction == null)
        //     {
        //         stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber, discountFactor, purchasePrice, factorDate);
        //         stockTransaction.WarehouseId = WarehouseId;
        //         await _context.StockTransactions.AddAsync(stockTransaction);
        //     }
        //     else
        //     {
        //         isAlreadyExist = true;
        //         isWarehouseChanged = stockTransaction.WarehouseId != WarehouseId;
        //         lastQuantity = stockTransaction.Quantity;
        //         lastWarehouse = stockTransaction.WarehouseId;
        //         stockTransaction.Quantity = quantity;
        //         stockTransaction.WarehouseId = WarehouseId;
        //         _context.StockTransactions.Update(stockTransaction);
        //     }
        //     if (isWarehouseChanged)
        //     {
        //         var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == lastWarehouse);
        //         if (stockRecord != null)
        //         {
        //             stockRecord.ReduceQuantity(lastQuantity);
        //             _context.Stocks.Update(stockRecord);
        //         }
        //         var newStockRecord = new Stock(productId, WarehouseId);
        //         newStockRecord.IncreaseQuantity(quantity);
        //         _context.Stocks.Add(newStockRecord);
        //     }
        //     else
        //     {
        //         var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId);
        //         if (stockRecord != null)
        //         {
        //             if (isAlreadyExist)
        //             {
        //                 if (quantity == 0)
        //                 {
        //                     stockRecord.ReduceQuantity(lastQuantity);
        //                 }
        //                 else if (quantity > lastQuantity)
        //                 {
        //                     decimal dif = quantity - lastQuantity;
        //                     stockRecord.IncreaseQuantity(dif);
        //                 }
        //                 else if (quantity < lastQuantity)
        //                 {
        //                     decimal dif = lastQuantity - quantity;
        //                     stockRecord.ReduceQuantity(dif);
        //                 }
        //             }
        //             _context.Stocks.Update(stockRecord);
        //         }
        //         else
        //         {
        //             stockRecord = new Stock(productId, WarehouseId);
        //             stockRecord.IncreaseQuantity(quantity);
        //             _context.Stocks.Add(stockRecord);
        //         }
        //     }
        //     await _context.SaveChangesAsync();
        // }
    }
}
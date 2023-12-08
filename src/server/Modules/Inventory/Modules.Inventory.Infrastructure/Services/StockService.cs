// --------------------------------------------------------------------------------------------------
// <copyright file="StockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Modules.Inventory.Core.Features.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Result<long>> RecordTransaction(StockTransactionDto stockTransactionDto)
        {
            var command = _mapper.Map<RecordTransactionCommand>(stockTransactionDto);
            return await _mediator.Send(command);
        }

        public async Task<Result<long>> RecordTransaction(List<StockTransactionDto> Transactions)
        {
            return await _mediator.Send(new MultipleRecordTransactionCommand(Transactions));
        }

        public async Task<List<WarehouseStockStatsDto>> GetStockBySKU(List<string> sku)
        {
            return await _mediator.Send(new GetStockBySKUs(sku));
        }

        public async Task<List<WarehouseStockStatsDto>> GetStockByVariantIds(List<long> variantIds)
        {
            return await _mediator.Send(new GetStockByVariantIds(variantIds));
        }

        public async Task<IGrouping<long, WarehouseStockStatsDto>> CheckInventory(Dictionary<string, int> skuQty)
        {
            List<string> skuList = new List<string>(skuQty.Keys);
            var warehouseStockStats = await GetStockBySKU(skuList);

            // filter warehouse with required qty
            warehouseStockStats = GetValidQtyWarehouse(warehouseStockStats, skuQty);

            // TODO: calculate distance based on lat,long
            var validQtyStockStats = CalculateDistance(warehouseStockStats);

            // final picked warehouse base on near by available stock
            return FinalWarehousePick(validQtyStockStats, skuQty.Count);
        }

        public async Task<IGrouping<long, WarehouseStockStatsDto>> CheckInventory(Dictionary<long, long> variantQty, List<long> skipWarehouses = null)
        {
            List<long> variantIds = new List<long>(variantQty.Keys);
            var warehouseStockStats = await GetStockByVariantIds(variantIds);

            if (skipWarehouses != null && skipWarehouses.Count > 0)
            {
                warehouseStockStats = warehouseStockStats.Where(x => !skipWarehouses.Contains(x.warehouseId)).ToList();
            }

            // filter warehouse with required qty
            warehouseStockStats = GetValidQtyWarehouse(warehouseStockStats, variantQty);

            // TODO: calculate distance based on lat,long
            var validQtyStockStats = CalculateDistance(warehouseStockStats);

            // final picked warehouse base on near by available stock
            return FinalWarehousePick(validQtyStockStats, variantQty.Count);
        }

        public List<WarehouseStockStatsDto> GetValidQtyWarehouse(List<WarehouseStockStatsDto> warehouseStockStats, Dictionary<long, long> skuQty)
        {
            List<WarehouseStockStatsDto> filterList = new List<WarehouseStockStatsDto>();

            foreach (var item in skuQty)
            {
                filterList.AddRange(warehouseStockStats.Where(x => x.inventoryItemId == item.Key && x.quantity >= item.Value).ToList());
            }

            return filterList;
        }

        public List<WarehouseStockStatsDto> GetValidQtyWarehouse(List<WarehouseStockStatsDto> warehouseStockStats, Dictionary<string, int> skuQty)
        {
            List<WarehouseStockStatsDto> filterList = new List<WarehouseStockStatsDto>();

            foreach (var item in skuQty)
            {
                filterList.AddRange(warehouseStockStats.Where(x => x.SKU == item.Key && x.quantity >= item.Value).ToList());
            }

            return filterList;
        }

        public List<IGrouping<long, WarehouseStockStatsDto>> CalculateDistance(List<WarehouseStockStatsDto> warehouseStockStats)
        {
            return warehouseStockStats.OrderBy(x => x.Distance).GroupBy(x => x.warehouseId).ToList();
        }

        public IGrouping<long, WarehouseStockStatsDto> FinalWarehousePick(List<IGrouping<long, WarehouseStockStatsDto>> ValidQtyStockStats, int skuCount)
        {
            var warehouseWithAllProducts = ValidQtyStockStats.Where(x => x.Count() >= skuCount).ToList();
            return warehouseWithAllProducts.FirstOrDefault();
        }

        // public async Task RecordTransaction(long productId, decimal quantity, string referenceNumber, OrderType transactionType, decimal discountFactor, decimal purchasePrice, DateTime factorDate, long WarehouseId)
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
        // public async Task ReverseTransaction(long productId, decimal quantity, string referenceNumber, OrderType orderType, long WarehouseId)
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
        // public async Task RecordOpeningTransaction(long productId, decimal quantity, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate, long WarehouseId)
        // {
        //     // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.
        //     OrderType transactionType = OrderType.OpeningStock;
        //     decimal lastQuantity = 0;
        //     long lastWarehouse = default(long);
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
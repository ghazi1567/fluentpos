// --------------------------------------------------------------------------------------------------
// <copyright file="StockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    /// <inheritdoc/>
    public class StockService : IStockService
    {
        private readonly IInventoryDbContext _context;

        /// <summary>
        /// Stock Service.
        /// </summary>
        /// <param name="context">Context.</param>
        public StockService(
            IInventoryDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, bool isSale = true)
        {
            // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.

            var transactionType = isSale ? OrderType.StockOut : OrderType.StockIn;
            var stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber);
            await _context.StockTransactions.AddAsync(stockTransaction);

            var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId);
            switch (transactionType)
            {
                case OrderType.StockOut:
                    if (stockRecord != null)
                    {
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId);
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;
                case OrderType.StockIn:
                    if (stockRecord != null)
                    {
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId);
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType transactionType, decimal discountFactor, decimal purchasePrice, DateTime factorDate, Guid WarehouseId)
        {
            // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.

            var stockTransaction = new StockTransaction(productId, quantity, transactionType, referenceNumber, discountFactor, purchasePrice, factorDate);
            stockTransaction.WarehouseId = WarehouseId;
            await _context.StockTransactions.AddAsync(stockTransaction);

            var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId);
            switch (transactionType)
            {
                case OrderType.StockOut:
                    if (stockRecord != null)
                    {
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId, WarehouseId);
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;
                case OrderType.StockIn:
                case OrderType.StockReturn:
                    if (stockRecord != null)
                    {
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId, WarehouseId);
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ReverseTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType orderType, Guid WarehouseId)
        {
            // TODO - Move this to MediatR, maybe? - Important, DO NOT make an API endpoint for this.

            var stockTransaction = _context.StockTransactions.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId && s.ReferenceNumber == referenceNumber && s.Type == orderType);
            if(stockTransaction != null)
            {
                _context.StockTransactions.Remove(stockTransaction);
            }

            var stockRecord = _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.WarehouseId == WarehouseId);
            switch (orderType)
            {
                case OrderType.StockOut:
                    if (stockRecord != null)
                    {
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId, WarehouseId);
                        stockRecord.IncreaseQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;
                case OrderType.StockIn:
                case OrderType.StockReturn:
                    if (stockRecord != null)
                    {
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Update(stockRecord);
                    }
                    else
                    {
                        stockRecord = new Stock(productId, WarehouseId);
                        stockRecord.ReduceQuantity(quantity);
                        _context.Stocks.Add(stockRecord);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(orderType), orderType, null);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateFactor(List<ProductFactorDto> productFactorDtos, DateTime updateFrom)
        {
            var productIds = productFactorDtos.Select(x => x.ProductId).Distinct().ToList();

            var stockRecord = _context.StockTransactions.Where(s => s.Timestamp >= updateFrom && productIds.Contains(s.ProductId)).ToList();

            foreach (var item in stockRecord)
            {
                var product = productFactorDtos.SingleOrDefault(x => x.ProductId == item.ProductId);
                if (product != null)
                {
                    item.FactorDate = product.FactorDate;
                    item.DiscountFactor = product.FactorAmount;
                }
            }

            _context.StockTransactions.UpdateRange(stockRecord);
            await _context.SaveChangesAsync();
        }
    }
}
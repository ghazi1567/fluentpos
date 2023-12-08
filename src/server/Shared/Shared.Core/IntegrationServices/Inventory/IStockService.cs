// --------------------------------------------------------------------------------------------------
// <copyright file="IStockService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Inventory
{
    /// <summary>
    /// Integration Services for Inventory Module.
    /// </summary>
    public interface IStockService
    {
        Task<Result<long>> RecordTransaction(StockTransactionDto stockTransactionDto);

        Task<Result<long>> RecordTransaction(List<StockTransactionDto> Transactions);

        Task<IGrouping<long, WarehouseStockStatsDto>> CheckInventory(Dictionary<string, int> skuQty);

        Task<IGrouping<long, WarehouseStockStatsDto>> CheckInventory(Dictionary<long, long> skuQty, List<long> skipWarehouses = null);

        Task<List<WarehouseStockStatsDto>> GetStockByVariantIds(List<long> variantIds);

        /// <summary>
        /// Record Transaction.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <param name="quantity">Quantity.</param>
        /// <param name="referenceNumber">Reference Number.</param>
        /// <param name="isSale">Is Sale.</param>
        /// <returns>Task Completed.</returns>
        // public Task RecordTransaction(long productId, decimal quantity, string referenceNumber, bool isSale = true);

        // public Task RecordTransaction(long productId, decimal quantity, string referenceNumber, OrderType transactionType, decimal discountFactor, decimal purchasePrice, DateTime factorDate, long WarehouseId);

        // public Task ReverseTransaction(long productId, decimal quantity, string referenceNumber, OrderType orderType, long WarehouseId);
        // public Task UpdateFactor(List<ProductFactorDto> productFactorDtos, DateTime updateFrom);
        // Task RecordOpeningTransaction(long productId, decimal quantity, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate, long WarehouseId);
    }
}
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
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Inventory
{
    /// <summary>
    /// Integration Services for Inventory Module.
    /// </summary>
    public interface IStockService
    {


        Task<Result<Guid>> RecordTransaction(StockTransactionDto stockTransactionDto);

        /// <summary>
        /// Record Transaction.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <param name="quantity">Quantity.</param>
        /// <param name="referenceNumber">Reference Number.</param>
        /// <param name="isSale">Is Sale.</param>
        /// <returns>Task Completed.</returns>
        // public Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, bool isSale = true);

        // public Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType transactionType, decimal discountFactor, decimal purchasePrice, DateTime factorDate, Guid WarehouseId);

        // public Task ReverseTransaction(Guid productId, decimal quantity, string referenceNumber, OrderType orderType, Guid WarehouseId);
        // public Task UpdateFactor(List<ProductFactorDto> productFactorDtos, DateTime updateFrom);
        // Task RecordOpeningTransaction(Guid productId, decimal quantity, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate, Guid WarehouseId);
    }
}
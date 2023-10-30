// --------------------------------------------------------------------------------------------------
// <copyright file="IInventoryDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Modules.Inventory.Core.Dtos;

namespace FluentPOS.Modules.Inventory.Core.Abstractions
{
    public interface IInventoryDbContext : IDbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockTransaction> StockTransactions { get; set; }

        public DbSet<ImportFile> ImportFiles { get; set; }

        public DbSet<ImportRecord> ImportRecords { get; set; }

        public DbSet<InternalInventoryLevel> InventoryLevels { get; set; }
    }
}
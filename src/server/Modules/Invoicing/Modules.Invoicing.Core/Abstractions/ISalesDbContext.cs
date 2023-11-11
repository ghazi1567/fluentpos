// --------------------------------------------------------------------------------------------------
// <copyright file="ISalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Invoicing.Core.Abstractions
{
    public interface ISalesDbContext : IDbContext
    {
        public DbSet<InternalOrder> Orders { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<POProduct> POProducts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<SyncLog> SyncLogs { get; set; }

        public DbSet<OrderFulfillment> OrderFulfillment { get; set; }

        public DbSet<OperationCity> OperationCity { get; set; }
    }
}
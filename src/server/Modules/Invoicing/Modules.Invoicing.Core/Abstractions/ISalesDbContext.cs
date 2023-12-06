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

        public DbSet<IntenalFulfillment> OrderFulfillment { get; set; }

        public DbSet<OperationCity> OperationCity { get; set; }

        public DbSet<InternalFulfillmentOrder> FulfillmentOrders { get; set; }

        public DbSet<InternalAddress> Addresses { get; set; }

        public DbSet<LoadSheetMain> LoadSheetMains { get; set; }

        public DbSet<LoadSheetDetail> LoadSheetDetails { get; set; }

        public DbSet<OrderLogs> OrderLogs { get; set; }

    }
}
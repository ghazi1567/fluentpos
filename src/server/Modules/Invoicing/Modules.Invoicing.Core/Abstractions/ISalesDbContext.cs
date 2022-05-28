// --------------------------------------------------------------------------------------------------
// <copyright file="ISalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Abstractions
{
    public interface ISalesDbContext : IDbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<POProduct> POProducts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<SyncLog> SyncLogs { get; set; }
    }
}
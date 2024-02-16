// --------------------------------------------------------------------------------------------------
// <copyright file="SalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Dapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Persistence
{
    public sealed class SalesDbContext : ModuleDbContext, ISalesDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;
        private readonly IJsonSerializer _json;

        protected override string Schema => "Invoicing";

        public SalesDbContext(
            DbContextOptions<SalesDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options, mediator, eventLogger, persistenceOptions, json)
        {
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        public DbSet<InternalOrder> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<POProduct> POProducts { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<SyncLog> SyncLogs { get; set; }

        public DbSet<IntenalFulfillment> OrderFulfillment { get; set; }

        public DbSet<OperationCity> OperationCity { get; set; }

        public DbSet<InternalFulfillmentOrder> FulfillmentOrders { get; set; }

        public DbSet<InternalAddress> Addresses { get; set; }

        public DbSet<LoadSheetMain> LoadSheetMains { get; set; }

        public DbSet<LoadSheetDetail> LoadSheetDetails { get; set; }

        public DbSet<OrderLogs> OrderLogs { get; set; }

        public DbSet<InternalLineItem> LineItems { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<InternalFulfillmentOrderLineItem> FulfillmentOrderLineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyApplicationConfiguration(_persistenceOptions);
        }

        public async Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string query, object parms = null)
        {
            string connectionString = string.Empty;
            if (_persistenceOptions.UseMsSql)
            {
                connectionString = _persistenceOptions.ConnectionStrings.MSSQL;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<TResponse>(query, parms, commandType: CommandType.StoredProcedure, commandTimeout: 15000);
                return result.ToList();
            }
        }

        public async Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string conStr, string query, object parms)
        {
            using (var connection = new SqlConnection(conStr))
            {
                var result = await connection.QueryAsync<TResponse>(query, parms, commandType: CommandType.StoredProcedure, commandTimeout: 15000);
                return result.ToList();
            }
        }

        public async Task<List<TResponse>> ExecuteQueryAsync<TResponse>(string query, object parms = null)
        {
            string connectionString = string.Empty;
            if (_persistenceOptions.UseMsSql)
            {
                connectionString = _persistenceOptions.ConnectionStrings.MSSQL;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<TResponse>(query, parms, commandType: CommandType.Text, commandTimeout: 15000);
                return result.ToList();
            }
        }

    }
}
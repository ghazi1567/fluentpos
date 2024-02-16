// --------------------------------------------------------------------------------------------------
// <copyright file="SalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Dapper;
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Infrastructure.Persistence
{
    public sealed class OrganizationDbContext : ModuleDbContext, IOrganizationDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;
        private readonly IJsonSerializer _json;

        protected override string Schema => "Org";

        public OrganizationDbContext(
            DbContextOptions<OrganizationDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options, mediator, eventLogger, persistenceOptions, json)
        {
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobHistory> JobHistory { get; set; }

        public DbSet<StoreWarehouse> StoreWarehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
// --------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        protected string Schema => "Application";

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<PersistenceSettings> persistenceOptions)
                : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<EntityReference> EntityReferences { get; set; }

        public DbSet<RemoteClient> RemoteClients { get; set; }

        public DbSet<WebhookEvent> WebhookEvents { get; set; }

        public string OperationName { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyApplicationConfiguration(_persistenceOptions);
        }

        public Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string query, object parms = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string conStr, string query, object parms)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TResponse>> ExecuteQueryAsync<TResponse>(string query, object parms = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="CatalogDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Infrastructure.Extensions;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Accounting.Infrastructure.Persistence
{
    public sealed class AccountingDbContext : ModuleDbContext, IAccountingDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        private readonly IJsonSerializer _json;

        protected override string Schema => "Accounting";

        public AccountingDbContext(
            DbContextOptions<AccountingDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options, mediator, eventLogger, persistenceOptions, json)
        {
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<PayrollRequest> PayrollRequests { get; set; }

        public DbSet<PayrollTransaction> PayrollTransactions { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<SalaryPerks> SalaryPerks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAccountingConfiguration(_persistenceOptions);
        }
    }
}
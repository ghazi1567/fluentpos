// --------------------------------------------------------------------------------------------------
// <copyright file="ICatalogDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Accounting.Core.Abstractions
{
    public interface IAccountingDbContext : IDbContext
    {
        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<PayrollRequest> PayrollRequests { get; set; }

        public DbSet<PayrollTransaction> PayrollTransactions { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<SalaryIncentiveDeduction> SalaryIncentiveDeductions { get; set; }
    }
}
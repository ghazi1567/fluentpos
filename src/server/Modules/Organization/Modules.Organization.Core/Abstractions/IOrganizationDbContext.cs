// --------------------------------------------------------------------------------------------------
// <copyright file="ISalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Modules.Organization.Core.Entities;

namespace FluentPOS.Modules.Organization.Core.Abstractions
{
    public interface IOrganizationDbContext : IDbContext
    {
        public DbSet<Store> Stores { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobHistory> JobHistory { get; set; }

        public DbSet<StoreWarehouse> StoreWarehouses { get; set; }
    }
}
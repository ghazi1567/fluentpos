// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Infrastructure.Persistence;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Organization.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<OrganizationDbContext>()
                 .AddScoped<ISalesDbContext>(provider => provider.GetService<OrganizationDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(OrganizationDbContext), Assembly.GetAssembly(typeof(ISalesDbContext)));

           
            return services;
        }

        public static IServiceCollection AddSalesValidation(this IServiceCollection services)
        {
            services.AddControllers().AddSalesValidation();
            return services;
        }
    }
}
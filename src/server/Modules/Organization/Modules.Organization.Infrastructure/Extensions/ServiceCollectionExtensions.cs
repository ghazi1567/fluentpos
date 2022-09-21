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
using FluentPOS.Modules.Organization.Infrastructure.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Organization.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<OrganizationDbContext>()
                 .AddScoped<IOrganizationDbContext>(provider => provider.GetService<OrganizationDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(OrganizationDbContext), Assembly.GetAssembly(typeof(IOrganizationDbContext)));
            services.AddScoped<IOrgService, OrgService>();
            return services;
        }

        public static IServiceCollection AddOrganizationValidation(this IServiceCollection services)
        {
            services.AddControllers().AddOrganizationValidation();
            return services;
        }
    }
}
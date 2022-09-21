// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Infrastructure.Persistence;

namespace FluentPOS.Modules.Accounting.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountingInfrastructure(this IServiceCollection services)
        {
            services
                .AddDatabaseContext<AccountingDbContext>()
                .AddScoped<IAccountingDbContext>(provider => provider.GetService<AccountingDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(AccountingDbContext), Assembly.GetAssembly(typeof(IAccountingDbContext)));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddAccountingValidation(this IServiceCollection services)
        {
            services.AddControllers().AddAccountingValidation();
            return services;
        }
    }
}
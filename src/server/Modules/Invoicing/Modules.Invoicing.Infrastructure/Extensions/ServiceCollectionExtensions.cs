// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Features.PO.Service;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Modules.Invoicing.Core.Services.BackgroundJob;
using FluentPOS.Modules.Invoicing.Infrastructure.Persistence;
using FluentPOS.Modules.Invoicing.Infrastructure.Services;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<SalesDbContext>()
                 .AddScoped<ISalesDbContext>(provider => provider.GetService<SalesDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(SalesDbContext), Assembly.GetAssembly(typeof(ISalesDbContext)));

            services.AddTransient<IPOService, POService>();
            services.AddTransient<ISyncService, SyncService>();
            services.AddHostedService<DataMigrationJob>();
            services.AddTransient<IShopifyOrderSyncJob, ShopifyOrderSyncJob>();
            services.AddTransient<IWebhookService, WebhookService>();
            services.AddTransient<IShopifyOrderService, ShopifyOrderService>();
            services.AddTransient<IShopifyLocationService, ShopifyLocationService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IShopifyOrderFulFillmentService, ShopifyOrderFulFillmentService>();
            services.AddTransient<IStoreService, StoreService>();

            return services;
        }

        public static IServiceCollection AddSalesValidation(this IServiceCollection services)
        {
            services.AddControllers().AddSalesValidation();
            return services;
        }
    }
}
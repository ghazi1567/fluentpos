// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Invoicing.Core.Extensions;
using FluentPOS.Modules.Invoicing.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Invoicing.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddInvoicingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInvoicingCore()
                .AddSalesInfrastructure()
                .AddSalesValidation();
            return services;
        }

        public static IApplicationBuilder UseInvoicingModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
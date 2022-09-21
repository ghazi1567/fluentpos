// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Accounting.Core.Extensions;
using FluentPOS.Modules.Accounting.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Accounting.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddAccountingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAccountingCore()
                .AddAccountingInfrastructure()
                .AddAccountingValidation();
            return services;
        }

        public static IApplicationBuilder UseAccountingModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
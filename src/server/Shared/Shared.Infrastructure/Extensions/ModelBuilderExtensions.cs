// --------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyApplicationConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            if (persistenceOptions.UseMsSql)
            {
                foreach (var property in builder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    bool hasColAttr = Attribute.IsDefined(property.PropertyInfo, typeof(ColumnAttribute));
                    if (!hasColAttr)
                    {
                        property.SetColumnType("decimal(23,2)");
                    }
                    else
                    {
                        var attr = (ColumnAttribute[])property.PropertyInfo.GetCustomAttributes(typeof(ColumnAttribute), false);
                        if (attr != null && attr.Length > 0)
                        {
                            string type = attr[0].TypeName;
                            string typeName = string.IsNullOrEmpty(type) ? "decimal(23,2)" : type;
                            property.SetColumnType(typeName);
                        }
                    }
                }
            }
        }

        public static void ApplyModuleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres
        }
    }
}
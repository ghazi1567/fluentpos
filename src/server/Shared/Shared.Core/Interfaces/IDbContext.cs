// --------------------------------------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();

        System.Threading.Tasks.Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string query, object parms = null);

        System.Threading.Tasks.Task<List<TResponse>> ExecuteProcedureAsync<TResponse>(string conStr, string query, object parms);

        Task<List<TResponse>> ExecuteQueryAsync<TResponse>(string query, object parms = null);
        public string OperationName { get; set; }

    }
}
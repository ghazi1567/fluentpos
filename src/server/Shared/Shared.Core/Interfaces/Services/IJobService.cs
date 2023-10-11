// --------------------------------------------------------------------------------------------------
// <copyright file="IJobService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IJobService
    {
        string Enqueue(Expression<Action> methodCall);

        string Enqueue(Expression<Func<Task>> methodCall);

        string Enqueue<T>(Expression<Action<T>> methodCall);

        string Enqueue<T>(Expression<Func<T, Task>> methodCall);

        string Schedule(Expression<Action> methodCall, TimeSpan delay);

        string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);

        string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt);

        string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt);

        string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

        string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

        string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt);

        string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);

        bool Delete(string jobId);

        bool Delete(string jobId, string fromState);

        bool Requeue(string jobId);

        bool Requeue(string jobId, string fromState);
    }
}
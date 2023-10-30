// --------------------------------------------------------------------------------------------------
// <copyright file="HangfireService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Interfaces.Services;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class HangfireService : IJobService
    {
        public bool Delete(string jobId) =>
           BackgroundJob.Delete(jobId);

        public bool Delete(string jobId, string fromState) =>
            BackgroundJob.Delete(jobId, fromState);

        public string Enqueue(Expression<Func<Task>> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public string Enqueue<T>(Expression<Action<T>> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public string Enqueue(Expression<Action> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall) =>
            BackgroundJob.Enqueue(methodCall);

        public bool Requeue(string jobId) =>
            BackgroundJob.Requeue(jobId);

        public bool Requeue(string jobId, string fromState) =>
            BackgroundJob.Requeue(jobId, fromState);

        public string Schedule(Expression<Action> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);

        public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);

        public string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);

        public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
            BackgroundJob.Schedule(methodCall, delay);

        public string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) =>
            BackgroundJob.Schedule(methodCall, enqueueAt);

        public void ScheduleRecurring(string recurringJobId, Expression<Func<Task>> methodCall, string cronExpression) =>
           RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);

        public void RemoveIfExists(string recurringJobId) =>
           RecurringJob.RemoveIfExists(recurringJobId);

    }
}
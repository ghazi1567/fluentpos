﻿// --------------------------------------------------------------------------------------------------
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
        string Enqueue(Expression<Func<Task>> methodCall);

        void Recurring(string jobName, Expression<Func<Task>> methodCall, string schdule = "");

        void ConfigureJob(JobType jobName, string schdule = "");

        void RunJob(JobType jobName, DateTime dateTime);
    }
}
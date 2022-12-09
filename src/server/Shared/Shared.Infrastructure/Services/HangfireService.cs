// --------------------------------------------------------------------------------------------------
// <copyright file="HangfireService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.Enums;
using Hangfire;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class HangfireService : IJobService
    {
        private readonly IAttendanceService _attendanceService;

        public HangfireService(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public string Enqueue(Expression<Func<Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public void Recurring(string jobName, Expression<Func<Task>> methodCall, string schdule = "")
        {
            if (string.IsNullOrEmpty(schdule))
            {
                schdule = Cron.Daily();
            }

            RecurringJob.AddOrUpdate(jobName, methodCall, schdule);
        }

        public void ConfigureJob(JobType jobName, string schdule = "")
        {
            if (string.IsNullOrEmpty(schdule))
            {
                schdule = Cron.Daily();
            }

            switch (jobName)
            {
                case JobType.MarkAbsent:
                    RecurringJob.AddOrUpdate("AbsentJob", () => _attendanceService.TiggerAutoAbsentJob(null), schdule);
                    break;
                case JobType.MarkOffDay:
                    break;
                case JobType.FetchCheckIn:
                    RecurringJob.AddOrUpdate("CheckInJob", () => _attendanceService.TiggerAutoPresentJob(null, true), schdule);
                    break; ;
                case JobType.FetchCheckOut:
                    RecurringJob.AddOrUpdate("CheckOutJob", () => _attendanceService.TiggerAutoPresentJob(null, false), schdule);
                    break;
                default:
                    break;
            }
        }

        public void RunJob(JobType jobName, DateTime dateTime)
        {
            switch (jobName)
            {
                case JobType.MarkAbsent:
                    _attendanceService.TiggerAutoAbsentJob(dateTime);
                    break;
                case JobType.MarkOffDay:
                    break;
                case JobType.FetchCheckIn:
                    _attendanceService.TiggerAutoPresentJob(dateTime, true);
                    break;
                case JobType.FetchCheckOut:
                    _attendanceService.TiggerAutoPresentJob(dateTime, false);
                    break;
                default:
                    break;
            }
        }
    }
}
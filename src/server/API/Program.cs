// --------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Upload;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace FluentPOS.Bootstrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string folderName = Path.Combine("Files", UploadType.Logs.ToDescriptionString());
            string logsPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (string.IsNullOrEmpty(logsPath))
            {
                logsPath = "C:\\";
            }

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                 .WriteTo.File(
       System.IO.Path.Combine(logsPath, "Information", "diagnostics.txt"),
       restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 10 * 1024 * 1024,
       retainedFileCountLimit: 2,
       rollOnFileSizeLimit: true,
       shared: true,
       flushToDiskInterval: TimeSpan.FromSeconds(1))
                 .WriteTo.File(
       System.IO.Path.Combine(logsPath, "Error", "diagnostics.txt"),
       restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 10 * 1024 * 1024,
       retainedFileCountLimit: 2,
       rollOnFileSizeLimit: true,
       shared: true,
       flushToDiskInterval: TimeSpan.FromSeconds(1))
                 .WriteTo.File(
       System.IO.Path.Combine(logsPath, "Critical", "diagnostics.txt"),
       restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Fatal,
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 10 * 1024 * 1024,
       retainedFileCountLimit: 2,
       rollOnFileSizeLimit: true,
       shared: true,
       flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
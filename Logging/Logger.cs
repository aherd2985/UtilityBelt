//---------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Evicio (Pvt) Ltd.">
//  Copyright (c) hacktoberfest-2020. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------------

namespace UtilityBelt.Logging
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Logging;

    public static class Logger
    {
        public static ILogger<T> InitLogger<T>() where T : class
        {
            var path =  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("SampleApp.Program", LogLevel.Debug)
                       .AddFile($"{path}\\Logs\\Log.txt");
            });

            var logger = loggerFactory.CreateLogger<T>();
            return logger;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLoanProject
{
    public class Program
    {
        //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          //  .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            //.Build();

        public static void Main(string[] args)
        {
           
            //var logger = NLogBuilder.ConfigureNLog("Nlog.Config").GetCurrentClassLogger();
            //logger.Debug("Main function");
            //CreateHostBuilder(args).Build().Run();
            //NLog.LogManager.Shutdown();

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configurationRoot).CreateLogger();
            CreateHostBuilder(args).Build().Run();



            //string connectionString = Configuration.GetConnectionString("UserDbConnection");

            //var columnOptions = new ColumnOptions
            //{
            //  AdditionalColumns = new CollectionExtensions<SqlColumn>
            //{
            //  new SqlColumn("UserName",SqlDbType.VarChar)
            //}
            //};

            //Log.Logger = new LoggerConfiguration()
            //   .Enrich.FromLogContext()
            // .WriteTo.MSSqlServer(connectionString, sinkOptions: new SinkOptions { TableName = "Logs" }
            //, null, null, LogEventLevel.Information, null, columnOptions, null, null).CreateLogger();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                });
    }
}

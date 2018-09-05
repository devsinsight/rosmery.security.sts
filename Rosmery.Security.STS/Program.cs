using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace Rosmery.Security.STS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Rosmery Security";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseKestrel(options =>
                //{
                //    options.Listen(IPAddress.Loopback, 5010, listenOptions =>
                //    {
                //        var serverCertificate = DevelopmentCertification.Get();
                //        listenOptions.UseHttps(serverCertificate);
                //    });
                //})
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
            
                .Build();

    }

    
}

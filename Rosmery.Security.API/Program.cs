using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Rosmery.Security.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Rosmery API";

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    // Set properties and call methods on options
                });
    }
}

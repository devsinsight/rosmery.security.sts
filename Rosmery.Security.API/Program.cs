using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Rosmery.Security.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Rosmery API";

            CreateWebHostBuilder(args).Build().Run();
        }

        private static X509Certificate2 GetCert() {
            using (FileStream certificateStream = File.Open(Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path"), FileMode.Open))
            {
                byte[] certificatePayload;
                using (var memoryStream = new MemoryStream())
                {
                    certificateStream.CopyTo(memoryStream);
                    certificatePayload = memoryStream.ToArray();
                }

                return new X509Certificate2(certificatePayload, Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    options.Listen(IPAddress.Any, 80);
                    options.Listen(IPAddress.Any, 443, listenOptions =>
                    {
                        
                        listenOptions.UseHttps(GetCert());
                    });
                });
    }
}

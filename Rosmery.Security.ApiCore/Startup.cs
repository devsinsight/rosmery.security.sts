using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rosmery.Security.ApiCore.Data;
using Rosmery.Security.Core.Configuration;
using System.Reflection;

namespace Rosmery.Security.ApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("SecurityDbConnection");

            services.AddIdentityConfiguration(assemblyName, connectionString);

            services
                .AddMvcCore()
                .AddJsonFormatters();

            services.AddIdentityServerServiceConfiguration(assemblyName, connectionString, "Security", DevelopmentCertification.GetFromStore());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TestData.InitializeDatabase(app);
            app.AddIdentityServerAppConfiguration();
            app.UseMvc();
        }

    }
}

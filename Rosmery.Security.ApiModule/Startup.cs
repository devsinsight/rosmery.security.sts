using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.ApiModule.IdentityServer;
using Rosmery.Security.Core.Configuration;
using System.Reflection;

namespace Rosmery.Security.ApiModule
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("SecurityDbConnection");

            services.AddIdentityConfiguration(assemblyName, connectionString);
            services.AddSingleton<IClientCredentials, ClientCredentials>();
            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddAccessTokenValidationServiceConfiguration();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

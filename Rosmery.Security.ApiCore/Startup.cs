using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.ApiCore.Data;
using Rosmery.Security.Core.Configuration;

namespace Rosmery.Security.ApiCore
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
            IdentityServiceConfiguration.Add(services, Configuration);

            services
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
            
            IdentityServer4ServiceConfiguration.Add(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IdentityServer4AppConfiguration.Add(app);

            TestData.InitializeDatabase(app);

            app.UseMvc();
        }

    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.Configuration;

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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            IdentityServiceConfiguration.Add(services, Configuration);

            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            IdentityServer4AccessTokenValidationServiceConfiguration.Add(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

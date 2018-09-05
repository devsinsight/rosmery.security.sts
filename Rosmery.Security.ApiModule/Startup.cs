using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.ApiModule.IdentityServer;
using Rosmery.Security.Core.Configuration;
using System;
using System.Reflection;

namespace Rosmery.Security.ApiModule
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider service)
        {
            Configuration = configuration;
            Service = service;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider Service { get;  }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("SecurityDbConnection");

            services.AddIdentityConfiguration(assemblyName, connectionString);

            services.AddSingleton<IClientCredentials, ClientCredentials>();

            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddCors();

            services.AddAccessTokenValidationServiceConfiguration(new ApiResources(Configuration));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

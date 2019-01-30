using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.API.IdentityServer;
using Rosmery.Security.Identity.Configuration;
using System;
using System.Reflection;

namespace Rosmery.Security.API
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
            var connectionString = Configuration["ConnectionString"];

            services.AddIdentityConfiguration(assemblyName, connectionString);

            services.AddCors();

            services.AddAccessTokenValidationServiceConfiguration(new ApiResources(Configuration));

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            //if (env.IsDevelopment())
            //{
            //    app.UseDatabaseErrorPage();
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHsts();

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}

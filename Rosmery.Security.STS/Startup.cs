using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rosmery.Security.Identity.Configuration;
using Rosmery.Security.STS.Configuration;
using Rosmery.Security.STS.Data;
using System.Reflection;

namespace Rosmery.Security.STS
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
            var connectionString = Configuration["ConnectionString"];

            services.AddIdentityConfiguration(assemblyName, connectionString);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentityServerServiceConfiguration(assemblyName, connectionString, "Security", DevelopmentCertification.GetFromContainer());

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Initializer.EnsureMigration(app);
            Initializer.InitializeDatabase(app);

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.AddIdentityServerAppConfiguration();
            app.UseMvcWithDefaultRoute();
        }

    }
}

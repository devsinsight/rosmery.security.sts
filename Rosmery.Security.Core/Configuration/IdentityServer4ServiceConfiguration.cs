using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.IdentityModels;
using Rosmery.Security.Core.IdentityServerServices;
using System.Security.Cryptography.X509Certificates;

namespace Rosmery.Security.Core.Configuration
{
    public static class IdentityServer4ServiceConfiguration
    {
        public static void AddIdentityServerServiceConfiguration(this IServiceCollection services, string assemblyName, string connectionString, string securitySchema, X509Certificate2 certification = null)
        {
            services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = securitySchema;
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = securitySchema;

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                //.AddDeveloperSigningCredential()   // development environment
                .AddSigningCredential(certification) // production environment
                .AddAspNetIdentity<User>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.IdentityModels;

namespace Rosmery.Security.Core.Configuration
{
    public static class IdentityServer4ServiceConfiguration
    {
        public static void AddIdentityServerServiceConfiguration(this IServiceCollection services, string assemblyName, string connectionString, string securitySchema)
        {

            services.AddIdentityServer()
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
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>();
        }
    }
}

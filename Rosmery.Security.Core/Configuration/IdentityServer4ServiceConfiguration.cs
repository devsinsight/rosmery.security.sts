using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.IdentityModels;

namespace Rosmery.Security.Core.Configuration
{
    public class IdentityServer4ServiceConfiguration
    {
        public static void Add(IServiceCollection services, IConfiguration config)
        {
            var assemblyName = "Rosmery.Security.Api";
            var connectionString = config.GetConnectionString("SecurityDbConnection");
            var securitySchemaName = "Security";

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = securitySchemaName;
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = securitySchemaName;

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>();
        }
    }
}

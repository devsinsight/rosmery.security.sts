using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.DatabaseContext;
using Rosmery.Security.Core.IdentityManagers;
using Rosmery.Security.Core.IdentityModels;

namespace Rosmery.Security.Core.Configuration
{
    public class IdentityServiceConfiguration
    {

        public static void Add(IServiceCollection services, IConfiguration config)
        {
            var assemblyName = "Rosmery.Security.Api";
            var connectionString = config.GetConnectionString("SecurityDbConnection");

            services
                .AddDbContext<SecurityDbContext>(options =>
                    options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(assemblyName)));

            services
                .AddIdentity<User, Role>(
                        options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequiredLength = 8;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;
                        })
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<SecurityUserManager<User>>();

        }
    }
}

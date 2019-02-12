using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Identity.DatabaseContext;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;

namespace Rosmery.Security.Identity.Configuration
{
    public static class IdentityServiceConfiguration
    {

        public static void AddIdentityConfiguration(this IServiceCollection services, string assemblyName, string connectionString)
        {
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
                            options.SignIn.RequireConfirmedEmail = true;
                        })
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<SecurityUserManager<User>>()
                .AddRoleManager<SecurityRoleManager<Role>>();

        }
    }
}

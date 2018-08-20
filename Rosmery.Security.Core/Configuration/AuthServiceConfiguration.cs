
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Core.DatabaseContext;
using Rosmery.Security.Core.IdentityManagers;
using Rosmery.Security.Core.IdentityModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Rosmery.Security.Core.Configuration
{
    public class AuthServiceConfiguration
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

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = "Security";
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(assemblyName));
                    options.DefaultSchema = "Security";

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>();
        }
    }
}

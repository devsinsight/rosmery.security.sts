
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rosmery.Security.Identity.DatabaseContext;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Rosmery.Security.STS.Data
{
    public class SecurityMasterDataBackgroundService : BackgroundService
    {
        private const string SecurityRoleName = "SECURITY_ADMINISTRATOR";
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _servicesScopedFactory;

        public SecurityMasterDataBackgroundService(
            IConfiguration configuration,
            IServiceScopeFactory servicesScopedFactory)
        {
            _configuration = configuration;
            _servicesScopedFactory = servicesScopedFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await CreateSecurityDatabase();
            await CreateSecurityMasterData();
            await CreateSecurityAdministrator();
        }

        private async Task CreateSecurityAdministrator() {
            using (var scope = _servicesScopedFactory.CreateScope())
            {
                var user = GetAdminUser();
                var userManager = scope.ServiceProvider.GetRequiredService<SecurityUserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if (!userManager.Users.Any())
                { 
                    await CreateSecurityUser(userManager, user, _configuration["SecurityAdministratorPassword"]);
                    await CreateSecurityRole(roleManager);
                    await AsignSecurityRole(userManager, user);
                }
            }
        }

        private async Task CreateSecurityMasterData()
        {
            using (var serviceScope = _servicesScopedFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in GetClients())
                    {
                        await context.Clients.AddAsync(client.ToEntity());
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in GetIdentityResources())
                    {
                        await context.IdentityResources.AddAsync(resource.ToEntity());
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in GetApiResources())
                    {
                        await context.ApiResources.AddAsync(resource.ToEntity());
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task CreateSecurityDatabase()
        {
            using (var serviceScope = _servicesScopedFactory.CreateScope())
            {
                var identityContext = serviceScope.ServiceProvider.GetService<SecurityDbContext>();
                await identityContext.Database.MigrateAsync();

                var stsContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                await stsContext.Database.MigrateAsync();

                var persistentContext = serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>();
                await persistentContext.Database.MigrateAsync();
            }
        }

        private User GetAdminUser() =>
            new User
            {
                UserName = "shoutmetal",
                Email = "olivares.rojas.jose@gmail.com",
                FirstName = "José Luis",
                LastName = "Olivares Rojas",
                EmailConfirmed = true
            };

        private Role GetAdminRole { get; set; } =
            new Role
            {
                Name = SecurityRoleName,
                Description = "Security Administrator Role"
            };

        private async Task CreateSecurityUser(SecurityUserManager<User> userManager, User user, string password) =>
            await userManager.CreateAsync(user, password);

        private async Task CreateSecurityRole(RoleManager<Role> roleManager) => 
            await roleManager.CreateAsync(GetAdminRole);

        private async Task AsignSecurityRole(SecurityUserManager<User> userManager, User user) =>
            await userManager.AddToRoleAsync(user, SecurityRoleName);

        private IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
            };

        private IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "rosmery-security-api",
                    ClientName = "Rosmery Security API",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    IdentityTokenLifetime=1200,
                    AccessTokenLifetime=1200,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                    AllowRememberConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris =
                    {
                        "http://localhost:4200/signin-callback",
                        "http://localhost:4200/silent-renew-callback",
                        "http://localhost:3000/signin-callback", //docker version
                        "http://localhost:3000/silent-renew-callback" //docker version
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4200/signout-callback",
                        "http://localhost:3000/signout-callback" //docker version
                    },

                    ClientSecrets =
                    {
                        new Secret("rosmery-security-api-secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "rosmery-security-api"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200",//simple ui
                        "http://localhost:5001", //no ssl dev
                        "https://localhost:44390", //ssl dev
                        "http://localhost:3000" //docker dev
                    },
                    Properties = new Dictionary<string, string>()
                    {
                        { "IsManagementApp", "Y" }
                    }
                    

                }
            };

        private IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("rosmery-security-api", "Rosmery Security API", new[] { "name", "role" } )
                {
                    ApiSecrets = { new Secret("rosmery-security-api-secret".Sha256()) }
                }
            };


    }

}

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rosmery.Security.Identity.DatabaseContext;
using System.Collections.Generic;
using System.Linq;
using static IdentityServer4.IdentityServerConstants;

namespace Rosmery.Security.STS.Data
{
    public class Initializer
    {

        private static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
            };
        }

        private static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "rosmery-security",
                    ClientName = "Rosmery Security",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    IdentityTokenLifetime=1200,
                    AccessTokenLifetime=1200,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
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
                        new Secret("rosmery-security-secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "rosmery-security"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200",//simple ui
                        "http://localhost:5001", //no ssl dev
                        "https://localhost:44390", //ssl dev
                        "http://localhost:3000" //docker dev
                    }
                    
                }
            };
        }

        private static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("rosmery-security", "Rosmery Security", new[] { "name", "role" } )
                {
                    ApiSecrets = { new Secret("rosmery-security-secret".Sha256()) }
                }
            };
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

        public static void EnsureMigration(IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var identityContext = serviceScope.ServiceProvider.GetService<SecurityDbContext>();
                identityContext.Database.Migrate();

                var stsContext = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                stsContext.Database.Migrate();

                var persistentContext = serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>();
                persistentContext.Database.Migrate();
            }
        }


    }
}

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using static IdentityServer4.IdentityServerConstants;

namespace Rosmery.Security.ApiCore.Data
{
    public class TestData
    {
        #region Init Demo Data

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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 20 * 60,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
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
                        "http://localhost:5001"
                    }
                    
                },
                new Client
                {
                    ClientId = "rosmery-website",
                    ClientName = "Rosmery Website",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 20 * 60,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("rosmery-website-secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "rosmery-website"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:5002"
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
                },
                new ApiResource("rosmery-website", "Rosmery Website", new[] { "name", "role" } )
                {
                    ApiSecrets = { new Secret("rosmery-website-secret".Sha256()) }
                },
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

        #endregion

    }
}

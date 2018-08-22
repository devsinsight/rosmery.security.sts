using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rosmery.Security.Core.Configuration
{
    public class IdentityServer4AccessTokenValidationServiceConfiguration
    {
        public static void Add(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000";
                        options.RequireHttpsMetadata = false;

                        options.ApiName = "rosmery-security";
                        options.ApiSecret = "rosmery-security-secret";
                        options.SupportedTokens = SupportedTokens.Both;
                        options.NameClaimType = "name";
                        options.RoleClaimType = "role";
                    });
        }
    }
}

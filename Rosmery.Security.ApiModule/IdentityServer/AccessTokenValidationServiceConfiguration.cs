using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rosmery.Security.ApiModule.IdentityServer
{
    public static class AccessTokenValidationServiceConfiguration
    {
        public static void AddAccessTokenValidationServiceConfiguration(this IServiceCollection services)
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
                        options.EnableCaching = false;
                        options.ApiName = "rosmery-security";
                        options.ApiSecret = "rosmery-security-secret";
                        options.SupportedTokens = SupportedTokens.Reference;
                        options.NameClaimType = "name";
                        options.RoleClaimType = "role";
                        
                    });
        }
    }
}

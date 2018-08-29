using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Rosmery.Security.ApiModule.IdentityServer
{
    public static class AccessTokenValidationServiceConfiguration
    {
        public static void AddAccessTokenValidationServiceConfiguration(this IServiceCollection services, IApiResources apiResources)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = "Cookies";
                        options.DefaultChallengeScheme = "oidc";
                    })
                    .AddCookie("Cookies")
                    .AddOpenIdConnect("oidc", options =>
                    {
                        options.SignInScheme = "Cookies";

                        options.Authority = apiResources.Authority;
                        options.RequireHttpsMetadata = false;
                        
                        options.ClientId = "rosmery-security";
                        options.ClientSecret = "rosmery-security-secret";
                        options.ResponseType = "code id_token";
                        options.SaveTokens = true;
                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = "name",
                            RoleClaimType = "role"
                        };
                    }); 
        }
    }
}

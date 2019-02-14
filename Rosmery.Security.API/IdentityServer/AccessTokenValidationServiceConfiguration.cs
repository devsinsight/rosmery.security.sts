﻿using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace Rosmery.Security.API.IdentityServer
{
    public static class AccessTokenValidationServiceConfiguration
    {
        public static void AddAccessTokenValidationServiceConfiguration(this IServiceCollection services, IApiResources apiResources)
        {
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = apiResources.Authority;
                        options.RequireHttpsMetadata = false;
                        options.EnableCaching = false;
                        options.ApiName = apiResources.ApiName;
                        options.ApiSecret = apiResources.ApiSecret;
                        options.SupportedTokens = SupportedTokens.Reference;
                        options.NameClaimType = "name";
                        options.RoleClaimType = "role";

                    });
        }
    
    }
}

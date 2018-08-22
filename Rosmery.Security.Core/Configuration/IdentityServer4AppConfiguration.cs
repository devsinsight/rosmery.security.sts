using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Rosmery.Security.Core.Configuration
{
    public static class IdentityServer4AppConfiguration
    {
        public static void AddIdentityServerAppConfiguration(this IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }
}

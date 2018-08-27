using Microsoft.AspNetCore.Builder;

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

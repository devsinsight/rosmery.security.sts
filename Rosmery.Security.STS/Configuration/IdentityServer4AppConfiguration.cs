using Microsoft.AspNetCore.Builder;

namespace Rosmery.Security.STS.Configuration
{
    public static class IdentityServer4AppConfiguration
    {
        public static void AddIdentityServerAppConfiguration(this IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }
}

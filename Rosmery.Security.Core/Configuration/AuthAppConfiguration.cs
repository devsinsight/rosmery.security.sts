using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Rosmery.Security.Core.Configuration
{
    public class AuthAppConfiguration
    {
        public static void Add(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace Rosmery.Security.API.IdentityServer
{
    public class ApiResources : IApiResources
    {
        public ApiResources(IConfiguration config)
        {
            ApiName = config["Security:ApiName"];
            ApiSecret = config["Security:ApiSecret"];
            Authority = config["Security:SecurityEndpoint"];
        }

        public string ApiName { get; }
        public string ApiSecret { get; }
        public string Authority { get; }
    }
}

using Microsoft.Extensions.Configuration;

namespace Rosmery.Security.API.IdentityServer
{
    public class ApiResources : IApiResources
    {
        public ApiResources(IConfiguration config)
        {
            ApiName = config["SecurityApiName"];
            ApiSecret = config["SecurityApiSecret"];
            Authority = config["SecurityEndpoint"];
        }

        public string ApiName { get; }
        public string ApiSecret { get; }
        public string Authority { get; }
    }
}

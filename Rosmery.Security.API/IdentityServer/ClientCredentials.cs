using Microsoft.Extensions.Configuration;

namespace Rosmery.Security.API.IdentityServer
{
    public class ClientCredentials : IClientCredentials
    {
        public ClientCredentials(IConfiguration config) {
            ClientId = config["Security:ClientId"];
            ClientSecret = config["Security:ClientSecret"];
            SecurityEndpoint = config["Security:SecurityEndpoint"];
            ClientScope = config["Security:ClientScope"];
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
        public string SecurityEndpoint { get; }
        public string ClientScope { get; }
    }
}

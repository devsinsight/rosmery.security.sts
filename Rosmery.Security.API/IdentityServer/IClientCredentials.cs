namespace Rosmery.Security.API.IdentityServer
{
    public interface IClientCredentials
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string SecurityEndpoint { get; }
        string ClientScope { get; }
    }
}

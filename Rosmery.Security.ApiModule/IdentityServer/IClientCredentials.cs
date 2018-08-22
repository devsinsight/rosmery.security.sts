namespace Rosmery.Security.ApiModule.IdentityServer
{
    public interface IClientCredentials
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string SecurityEndpoint { get; }
        string ClientScope { get; }
    }
}

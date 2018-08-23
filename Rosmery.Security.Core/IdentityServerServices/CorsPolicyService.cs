using System.Threading.Tasks;
using IdentityServer4.Services;

namespace Rosmery.Security.Core.IdentityServerServices
{
    public class CorsPolicyService : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            throw new System.NotImplementedException();
        }
    }
}

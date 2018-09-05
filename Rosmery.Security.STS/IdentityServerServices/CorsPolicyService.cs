using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace Rosmery.Security.STS.IdentityServerServices
{
    public class CorsPolicyService : DefaultCorsPolicyService, ICorsPolicyService
    {
        private readonly ILoggerFactory _loggerFactory;

        public CorsPolicyService(ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<DefaultCorsPolicyService>())
        {
            _loggerFactory = loggerFactory;
        }

        public override Task<bool> IsOriginAllowedAsync(string origin)
        {
            AllowAll = false;

            return Task.FromResult(false);
        }
    }
}

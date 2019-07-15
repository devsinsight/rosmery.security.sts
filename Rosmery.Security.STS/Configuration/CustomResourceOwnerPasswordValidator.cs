using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace Rosmery.Security.STS.Configuration
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            return Task.CompletedTask;
        }
    }
}

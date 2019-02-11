using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.STS.Configuration
{
    public class CustomAuthorizeInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
    {
        public CustomAuthorizeInteractionResponseGenerator(ISystemClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) 
            :  base(clock, logger, consent, profile) { }

        protected async override Task<InteractionResponse> ProcessLoginAsync(ValidatedAuthorizeRequest request)
        {
            return await base.ProcessLoginAsync(request);
        }

    }
}

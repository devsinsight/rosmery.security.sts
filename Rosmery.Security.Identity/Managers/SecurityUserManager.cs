using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rosmery.Security.Identity.Managers
{
    public class SecurityUserManager<T> : UserManager<T> where T : User
    {
        private readonly IPasswordHasher<T> _passwordHasher;

        public SecurityUserManager(IUserStore<T> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<T> passwordHasher, IEnumerable<IUserValidator<T>> userValidators, IEnumerable<IPasswordValidator<T>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<T>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ValidateUserAsync(T user, string password)
        {
            return await Task.FromResult(PasswordVerificationResult.Success == _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password));
        }
    }
}

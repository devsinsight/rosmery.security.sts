using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rosmery.Security.Identity.Models;

namespace Rosmery.Security.Identity.Managers
{
    public class SecuritySignInManager<TUser> : SignInManager<TUser> where TUser : User
    {
        public SecuritySignInManager(
            UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {

        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            User user = new User()
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = "6f38b56f-811d-4845-92f8-80003f6f4c66",
                Email = "olivares.rojas.jose@gmail.com",
                EmailConfirmed = true,
                FirstName="José Luis",
                //Id= new Guid("73214198-aabe-442a-ff50-08d7088b2103"),
                Id = new Guid("c08cb766-74d0-4f34-af12-c2e0d8d96ac3"),
                LastName = "Olivares Rojas",
                LockoutEnabled = true,
                LockoutEnd=null,
                NormalizedEmail="OLIVARES.ROJAS.JOSE@GMAIL.COM",
                NormalizedUserName="SHOUTMETAL",
                PasswordHash="AQAAAAEAACcQAAAAECfZCp4yI2SHx4lRjjQIx6u2yDHk/jncwNhYj4BEXjN9Ly7vc5D5dL0LWVD14R2GQw==",
                PhoneNumber=null,
                PhoneNumberConfirmed = false,
                SecurityStamp="4KCWOXIHMSOKCJDNS6Y7YVGYI3L47STK",
                TwoFactorEnabled=false,
                UserName="shoutmetal"

            };


            await SignInAsync((TUser)user, true, null);
            return SignInResult.Success;

            //return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public override Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {

            //authenticationProperties.IsPersistent = true;
            //authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow;




            return base.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        public override Task<ClaimsPrincipal> CreateUserPrincipalAsync(TUser user)
        {
            return base.CreateUserPrincipalAsync(user);
        }


    }
}

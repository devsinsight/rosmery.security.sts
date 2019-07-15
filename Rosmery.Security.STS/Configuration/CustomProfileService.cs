using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rosmery.Security.STS.Configuration
{
    public class CustomProfileService : IProfileService
    {

        User user = new User()
        {
            AccessFailedCount = 0,
            ConcurrencyStamp = "6f38b56f-811d-4845-92f8-80003f6f4c66",
            Email = "olivares.rojas.jose@gmail.com",
            EmailConfirmed = true,
            FirstName = "José Luis",
            //Id= new Guid("73214198-aabe-442a-ff50-08d7088b2103"),
            Id = new Guid("c08cb766-74d0-4f34-af12-c2e0d8d96ac3"),
            LastName = "Olivares Rojas",
            LockoutEnabled = true,
            LockoutEnd = null,
            NormalizedEmail = "OLIVARES.ROJAS.JOSE@GMAIL.COM",
            NormalizedUserName = "SHOUTMETAL",
            PasswordHash = "AQAAAAEAACcQAAAAECfZCp4yI2SHx4lRjjQIx6u2yDHk/jncwNhYj4BEXjN9Ly7vc5D5dL0LWVD14R2GQw==",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            SecurityStamp = "4KCWOXIHMSOKCJDNS6Y7YVGYI3L47STK",
            TwoFactorEnabled = false,
            UserName = "shoutmetal"

        };


        protected readonly IUserClaimsPrincipalFactory<User> ClaimsFactory;

        protected readonly ILogger<ProfileService<User>> Logger;

        public CustomProfileService(IUserClaimsPrincipalFactory<User> claimsFactory)
        {

            ClaimsFactory = claimsFactory;
        }



        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

         
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");

            //var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }
            else
            {
                var principal = await ClaimsFactory.CreateAsync(user);
                if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");

                context.AddRequestedClaims(principal.Claims);
            }


        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject Id claim present");

            //var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }

    }
}

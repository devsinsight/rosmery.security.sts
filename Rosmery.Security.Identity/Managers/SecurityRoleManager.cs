using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosmery.Security.Identity.Managers
{
    public class SecurityRoleManager<TRole> : RoleManager<TRole> where TRole : Role
    {
        public SecurityRoleManager(IRoleStore<TRole> store, IEnumerable<IRoleValidator<TRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<TRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            
        }

        public IEnumerable<Role> GetAllRoles() => Roles;

        
    }


}

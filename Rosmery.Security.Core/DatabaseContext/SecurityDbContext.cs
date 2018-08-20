
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rosmery.Security.Core.IdentityModels;
using System;

namespace Rosmery.Security.Core.DatabaseContext
{
    public class SecurityDbContext : IdentityDbContext<User, Role, Guid>
    {

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Security");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable(nameof(RoleClaim));
            builder.Entity<IdentityUserClaim<Guid>>().ToTable(nameof(UserClaim));
            builder.Entity<IdentityUserLogin<Guid>>().ToTable(nameof(UserLogin));
            builder.Entity<IdentityUserToken<Guid>>().ToTable(nameof(UserToken));
            builder.Entity<IdentityUserRole<Guid>>().ToTable(nameof(UserRole));
            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<Role>().ToTable(nameof(Role));

            
        }
    }
}

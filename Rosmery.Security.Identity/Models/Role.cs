using Microsoft.AspNetCore.Identity;
using System;

namespace Rosmery.Security.Identity.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.API.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? HasUsers { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Rosmery.Security.API.Commands
{
    public class CreateRoleCommand : Command
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

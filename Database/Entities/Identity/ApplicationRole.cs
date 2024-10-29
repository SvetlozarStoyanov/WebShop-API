using Microsoft.AspNetCore.Identity;

namespace Database.Entities.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}

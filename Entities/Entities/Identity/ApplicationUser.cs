using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public override string? Email { get; set; }
        public override string? NormalizedEmail { get; set; }          
        public override string? PhoneNumber { get; set; }
    }
}

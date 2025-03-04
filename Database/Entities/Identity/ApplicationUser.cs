using Microsoft.AspNetCore.Identity;

namespace Database.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }

        public override string? Email { get; set; }
        public override string? NormalizedEmail { get; set; }          
        public override string? PhoneNumber { get; set; }
    }
}

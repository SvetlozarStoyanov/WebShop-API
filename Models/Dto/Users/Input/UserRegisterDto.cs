using Models.Dto.Customers;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Users.Input
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public CustomerRegisterDto Customer { get; set; }
    }
}

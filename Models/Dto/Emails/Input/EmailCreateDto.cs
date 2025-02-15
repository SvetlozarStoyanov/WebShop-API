using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Emails.Input
{
    public class EmailCreateDto
    {
        [Required]
        public string Address { get; set; }
        public bool IsMain { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Emails.Input
{
    public class EmailEditDto
    {
        public long Id { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsMain { get; set; }
    }
}

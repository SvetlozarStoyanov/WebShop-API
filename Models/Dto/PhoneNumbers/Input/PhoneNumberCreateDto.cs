using System.ComponentModel.DataAnnotations;

namespace Models.Dto.PhoneNumbers.Input
{
    public class PhoneNumberCreateDto
    {
        [Required]
        public string Number { get; set; }
        public bool IsMain { get; set; }
        public long CountryId { get; set; }
    }
}

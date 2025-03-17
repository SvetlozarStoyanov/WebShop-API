using System.ComponentModel.DataAnnotations;

namespace Models.Dto.PhoneNumbers
{
    public class PhoneNumberUpdateDto
    {
        public long? Id { get; set; }
        [Required]
        public string Number { get; set; }
        public bool IsMain { get; set; }
        public long CountryId { get; set; }
    }
}

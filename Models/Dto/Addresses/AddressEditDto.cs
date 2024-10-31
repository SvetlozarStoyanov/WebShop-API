using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Addresses
{
    public class AddressEditDto
    {
        public long Id { get; set; }
        [Required]
        public string AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
        public bool IsMain { get; set; }
        public long CountryId { get; set; }
    }
}

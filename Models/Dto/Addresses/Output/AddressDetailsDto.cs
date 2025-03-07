using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Addresses.Output
{
    public class AddressDetailsDto
    {
        public long Id { get; set; }
        public string AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool IsMain { get; set; }
        public long CountryId { get; set; }
    }
}

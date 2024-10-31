using Models.Dto.Addresses;
using Models.Dto.Emails;
using Models.Dto.PhoneNumbers;

namespace Models.Dto.Customers
{
    public class CustomerRegisterDto
    {
        public IEnumerable<AddressCreateDto> Addresses { get; set; }
        public IEnumerable<PhoneNumberCreateDto> PhoneNumbers { get; set; }
        public IEnumerable<EmailCreateDto> Emails { get; set; }
    }
}

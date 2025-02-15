using Models.Dto.Addresses.Input;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers.Input;

namespace Models.Dto.Customers
{
    public class CustomerRegisterDto
    {
        public IEnumerable<AddressCreateDto> Addresses { get; set; }
        public IEnumerable<PhoneNumberCreateDto> PhoneNumbers { get; set; }
        public IEnumerable<EmailCreateDto> Emails { get; set; }
    }
}

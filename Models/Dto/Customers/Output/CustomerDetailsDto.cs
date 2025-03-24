using Models.Dto.Addresses.Output;
using Models.Dto.Emails.Output;
using Models.Dto.PhoneNumbers.Output;
using Models.Dto.Users.Output;

namespace Models.Dto.Customers.Output
{
    public class CustomerDetailsDto
    {
        public UserDetailsDto User { get; set; }
        public IEnumerable<AddressDetailsDto> Addresses { get; set; }
        public IEnumerable<PhoneNumberDetailsDto> PhoneNumbers { get; set; }
        public IEnumerable<EmailDetailsDto> Emails { get; set; }
    }
}

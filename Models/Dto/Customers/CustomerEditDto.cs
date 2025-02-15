using Models.Dto.Addresses.Input;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers.Input;

namespace Models.Dto.Customers
{
    public class CustomerEditDto
    {
        public UpdateAddressesDto? UpdatedAddresses { get; set; }
        public UpdatePhoneNumbersDto? UpdatedPhoneNumbers { get; set; }
        public UpdateEmailsDto? UpdatedEmails { get; set; }
    }
}

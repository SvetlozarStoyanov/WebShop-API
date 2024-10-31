using Models.Dto.Addresses;
using Models.Dto.Emails;
using Models.Dto.PhoneNumbers;

namespace Models.Dto.Customers
{
    public class CustomerEditDto
    {
        public UpdateAddressesDto? UpdatedAddresses { get; set; }
        public UpdatePhoneNumbersDto? UpdatedPhoneNumbers { get; set; }
        public UpdateEmailsDto? UpdatedEmails { get; set; }
    }
}

using Database.Entities.Addresses;
using Database.Entities.Emails;
using Database.Entities.Identity;
using Database.Entities.PhoneNumbers;

namespace Database.Entities.Customers
{
    public class Customer
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}

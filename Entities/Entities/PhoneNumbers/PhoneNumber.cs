using Database.Entities.Common;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;

namespace Database.Entities.PhoneNumbers
{
    public class PhoneNumber
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public bool IsMain { get; set; }
        public long StatusId { get; set; }
        public PhoneNumberStatus Status { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

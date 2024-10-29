using Database.Entities.Common;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;

namespace Database.Entities.Addresses
{
    public class Address
    {
        public long Id { get; set; }
        public string AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool IsMain { get; set; }
        public long StatusId { get; set; }
        public AddressStatus Status { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

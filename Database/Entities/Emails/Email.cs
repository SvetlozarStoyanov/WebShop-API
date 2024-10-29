using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;

namespace Database.Entities.Emails
{
    public class Email
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public bool IsMain { get; set; }
        public long StatusId { get; set; }
        public EmailStatus Status { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

using Database.Entities.Identity;

namespace Database.Entities.Customers
{
    public class Customer
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

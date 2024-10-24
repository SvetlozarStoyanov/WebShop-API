using Database.Entities.Common.Statuses;
using Database.Entities.Customers;

namespace Database.Entities.Orders
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public long Id { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}

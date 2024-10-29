using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;

namespace Database.Entities.Orders
{
    public class Order
    {
        public Order()
        {
            Details = new List<OrderDetails>();
            Items = new List<OrderItem>();
        }

        public long Id { get; set; }     
        public long StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}

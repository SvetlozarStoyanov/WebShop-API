using Database.Entities.Common.Nomenclatures.Orders;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace Database.Entities.Orders
{
    public class OrderDetails
    {
        public long Id { get; set; }
        public long StageId  { get; set; }
        public OrderDetailsStage Stage  { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long StatusId { get; set; }
        public OrderDetailsStatus Status { get; set; }
    }
}

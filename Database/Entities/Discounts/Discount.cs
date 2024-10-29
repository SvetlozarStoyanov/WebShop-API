using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Products;

namespace Database.Entities.Discounts
{
    public class Discount
    {
        public long Id { get; set; }
        public decimal Percentage { get; set; }
        public int DurationInDays { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public long StatusId { get; set; }
        public DiscountStatus Status { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}

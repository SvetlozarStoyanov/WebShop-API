namespace Models.Dto.Orders
{
    public class OrderItemCreateDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
namespace Models.Dto.Orders.Input
{
    public class OrderItemCreateDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
namespace Models.Dto.Orders
{
    public class OrderDetailsCreateDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
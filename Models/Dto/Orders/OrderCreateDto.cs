namespace Models.Dto.Orders
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderItemCreateDto> Products { get; set; }
        
    }
}

namespace Models.Dto.Orders.Input
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderItemCreateDto> Products { get; set; }
        
    }
}

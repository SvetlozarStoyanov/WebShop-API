namespace Models.Dto.Orders
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderDetailsCreateDto> Products { get; set; }
        
    }
}

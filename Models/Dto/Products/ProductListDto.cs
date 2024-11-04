namespace Models.Dto.Products
{
    public class ProductListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string? Base64Image { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}

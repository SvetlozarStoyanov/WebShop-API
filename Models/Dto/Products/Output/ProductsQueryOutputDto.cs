using Models.Enums.QueryDto;

namespace Models.Dto.Products.Output
{
    public class ProductsQueryOutputDto
    {
        public ProductsQueryOutputDto()
        {
            CategoryIds = new HashSet<long>();
        }

        public int ItemsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string? SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public ProductsOrdering Ordering { get; set; }
        public IEnumerable<long> CategoryIds { get; set; }
        public IEnumerable<ProductListDto> ProductListDtos { get; set; }
    }
}

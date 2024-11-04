using Models.Dto.Products;

namespace Contracts.Services.Entity.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetAllProductsAsync();

        Task<ProductsQueryOutputDto> GetFilteredProductsAsync(ProductsQueryInputDto productsQueryDto);
    }
}

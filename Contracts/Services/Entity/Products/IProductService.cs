using Models.Dto.Products.Input;
using Models.Dto.Products.Output;

namespace Contracts.Services.Entity.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetAllProductsAsync();

        Task<ProductsQueryOutputDto> GetFilteredProductsAsync(ProductsQueryInputDto productsQueryDto);
    }
}

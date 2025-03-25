using Database.Entities.Products;
using Models.Dto.Products.Input;
using Models.Dto.Products.Output;

namespace Contracts.Services.Entity.Products
{
    /// <summary>
    /// Performs <see cref="Product"/> related operations
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Returns all <see cref="Product"/>
        /// </summary>
        /// <returns><see cref="IEnumerable"/> of type <see cref="ProductListDto"/></returns>
        Task<IEnumerable<ProductListDto>> GetAllProductsAsync();

        /// <summary>
        /// Retuns <see cref="Product"/> which match the criteria given by <paramref name="productsQueryInputDto"/>
        /// </summary>
        /// <param name="productsQueryInputDto"></param>
        /// <returns><see cref="ProductsQueryOutputDto"/> productsQueryOutputDto</returns>
        Task<ProductsQueryOutputDto> GetFilteredProductsAsync(ProductsQueryInputDto productsQueryInputDto);
    }
}

using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Products;
using Contracts.Services.Managers.Products;
using Database.Entities.Orders;
using Models.Dto.Products.Input;
using Models.Dto.Products.Output;

namespace Services.Managers.Products
{
    /// <summary>
    /// Performs <see cref="Order"/> related operations
    /// </summary>
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;

        public ProductManager(IUnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<ProductListDto>> GetAllProductsAsync()
        {
            return await productService.GetAllProductsAsync();
        }

        ///<inheritdoc/>
        public async Task<ProductsQueryOutputDto> GetFilteredProductsAsync(ProductsQueryInputDto productsQueryDto)
        {
            return await productService.GetFilteredProductsAsync(productsQueryDto);
        }
    }
}

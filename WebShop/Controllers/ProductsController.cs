using Contracts.Services.Entity.Products;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Products.Input;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> Filter([FromQuery]ProductsQueryInputDto queryInputDto)
        {
            var queryOutputDto = await productService.GetFilteredProductsAsync(queryInputDto);

            return Ok(queryOutputDto);
        }
    
    }
}

using Contracts.Services.Managers.Products;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Products.Input;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager productManager;

        public ProductsController(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productManager.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> Filter([FromQuery]ProductsQueryInputDto queryInputDto)
        {
            var queryOutputDto = await productManager.GetFilteredProductsAsync(queryInputDto);

            return Ok(queryOutputDto);
        }
    
    }
}

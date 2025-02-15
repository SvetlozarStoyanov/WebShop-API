using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Products;
using Database.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Products.Input;
using Models.Dto.Products.Output;
using Models.Enums.QueryDto;

namespace Services.Entity.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductListDto>> GetAllProductsAsync()
        {
            var productDtos = await unitOfWork.ProductRepository.AllAsNoTracking()
                .Select(x => new ProductListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    QuantityInStock = x.QuantityInStock,
                    Price = x.Price,
                    Categories = x.Categories.Select(x => x.Name),
                    Base64Image = x.Base64Image
                })
                .ToListAsync();

            return productDtos;
        }

        public async Task<ProductsQueryOutputDto> GetFilteredProductsAsync(ProductsQueryInputDto productsQueryInputDto)
        {
            var productsQueryable = unitOfWork.ProductRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(productsQueryInputDto.SearchTerm))
            {
                productsQueryable = productsQueryable.Where(x => x.Name.ToLower().Contains(productsQueryInputDto.SearchTerm.ToLower()));
            }

            if (productsQueryInputDto.CategoryIds.Any())
            {
                foreach (var categoryId in productsQueryInputDto.CategoryIds)
                {
                    productsQueryable = productsQueryable.Where(x => x.Categories.Any(x => x.Id == categoryId));
                }
            }

            if (productsQueryInputDto.MinPrice.HasValue)
            {
                productsQueryable = productsQueryable.Where(x => x.Price >= productsQueryInputDto.MinPrice);
            }

            if (productsQueryInputDto.MaxPrice.HasValue)
            {
                productsQueryable = productsQueryable.Where(x => x.Price <= productsQueryInputDto.MaxPrice);
            }

            var productsQueryOutputDto = await CreateProductsQueryOutputDtoAsync(productsQueryInputDto, productsQueryable);

            return productsQueryOutputDto;
        }


        private async Task<ProductsQueryOutputDto> CreateProductsQueryOutputDtoAsync(ProductsQueryInputDto productsQueryInputDto,
            IQueryable<Product> productsQueryable)
        {
            var queryOutputDto = new ProductsQueryOutputDto();

            queryOutputDto.PageNumber = productsQueryInputDto.PageNumber;
            queryOutputDto.ItemsPerPage = productsQueryInputDto.ItemsPerPage;
            queryOutputDto.CategoryIds = productsQueryInputDto.CategoryIds;
            queryOutputDto.MinPrice = productsQueryInputDto.MinPrice;
            queryOutputDto.MaxPrice = productsQueryInputDto.MaxPrice;
            queryOutputDto.Ordering = productsQueryInputDto.Ordering;

            switch (productsQueryInputDto.Ordering)
            {
                case ProductsOrdering.Alphabetical:
                    productsQueryable = productsQueryable.OrderBy(x => x.Name);
                    break;
                case ProductsOrdering.PriceAscending:
                    productsQueryable = productsQueryable.OrderBy(x => x.Price);
                    break;
                case ProductsOrdering.PriceDescending:
                    productsQueryable = productsQueryable.OrderByDescending(x => x.Price);
                    break;
            }

            var productDtos = await productsQueryable
                .Skip((productsQueryInputDto.PageNumber - 1) * productsQueryInputDto.ItemsPerPage)
                .Take(productsQueryInputDto.ItemsPerPage)
                .Select(x => new ProductListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    QuantityInStock = x.QuantityInStock,
                    Price = x.Price,
                    Categories = x.Categories.Select(x => x.Name),
                    Base64Image = x.Base64Image
                })
                .ToListAsync();

            queryOutputDto.ProductListDtos = productDtos;

            return queryOutputDto;
        }
    }
}

﻿using Models.Enums.QueryDto;

namespace Models.Dto.Products
{
    public class ProductsQueryInputDto
    {
        public ProductsQueryInputDto()
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
    }
}
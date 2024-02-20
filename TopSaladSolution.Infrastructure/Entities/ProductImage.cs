using Microsoft.AspNetCore.Http;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string? ImagePath { get; set; }
        public string? Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public long FileSize { get; set; }
        public Product Product { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
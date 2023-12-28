using System.Collections.Generic;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        public int SubCategoryId { get; set; }
        public int Views { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        public SubCategory SubCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { set; get; } = new List<OrderDetail>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation> { };
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}

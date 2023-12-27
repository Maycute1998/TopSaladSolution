using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class Product : BaseEntity
    {
        public int SubCategoryId { get; set; }
        public int Views { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<OrderDetail> OrderDetails { set; get; }
        public List<Cart> Carts { get; set; }
        public List<ProductTranslation> ProductTranslations { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}

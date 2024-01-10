using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Model.Products
{
    public class ProductSoftDeleteRequest
    {
        public int Id { get; set; }
        public ItemStatus Status { get; set; }
    }
}

using System;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Cart : BaseEntity
    {
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Guid UserId { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}

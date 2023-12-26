namespace TopSaladSolution.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public DateTime DateCreated { get; set; }

        public Guid UserId { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}

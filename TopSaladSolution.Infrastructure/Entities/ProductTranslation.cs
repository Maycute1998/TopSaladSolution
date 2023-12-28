namespace TopSaladSolution.Infrastructure.Entities
{
    public class ProductTranslation : BaseEntity
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; } = null;
        public string? SeoTitle { get; set; } = null;
        public string? SeoAlias { get; set; } = null;
        public int LanguageId { get; set; } = 0;
        public Product Product { get; set; }
        public Language Language { get; set; }
    }
}

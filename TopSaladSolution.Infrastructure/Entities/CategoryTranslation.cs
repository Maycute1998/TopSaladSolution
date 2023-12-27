namespace TopSaladSolution.Infrastructure.Entities
{
    public class CategoryTranslation : BaseEntity
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoAlias { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
    }
}

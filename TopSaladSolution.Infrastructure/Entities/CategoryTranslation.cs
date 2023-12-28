namespace TopSaladSolution.Infrastructure.Entities
{
    public class CategoryTranslation : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string SeoDescription { get; set; } = null;
        public string SeoTitle { get; set; } = null;
        public string SeoAlias { get; set; } = null;
        public int LanguageId { get; set; } = 0;
        public Language Language { get; set; }
        public Category Category { get; set; }
    }
}

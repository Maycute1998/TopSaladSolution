namespace TopSaladSolution.Data.Entities
{
    public class Language : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsDefault { get; set; }
        public List<ProductTranslation> ProductTranslations { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}

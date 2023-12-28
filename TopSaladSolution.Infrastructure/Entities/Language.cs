using System.Collections.Generic;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation>();
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; } = new List<CategoryTranslation>();
        public ICollection<SubCategoryTranslation> SubCategoryTranslations { get; set; } = new List<CategoryTranslation>();
    }
}

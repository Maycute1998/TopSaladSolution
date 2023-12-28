using System.Collections.Generic;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Category : BaseEntity
    {
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; } = new List<CategoryTranslation>();
        public ICollection<SubCategory> SubCategories { get; } = new List<SubCategory>();
    }
}

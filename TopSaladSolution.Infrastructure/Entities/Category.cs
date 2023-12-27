using System.Collections.Generic;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Category : BaseEntity
    {
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }

    }
}

﻿using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }

    }
}

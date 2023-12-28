using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class SubCategory : BaseEntity
    {
        public int Order { get; set; }
        public bool IsShow { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SubCategoryTranslation> SubCategoryTranslations { get; set; } = new List<SubCategoryTranslation>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public Category Categories { get; set; }
    }
}

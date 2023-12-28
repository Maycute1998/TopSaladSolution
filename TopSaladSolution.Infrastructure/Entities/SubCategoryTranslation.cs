using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class SubCategoryTranslation : BaseEntity
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string SeoDescription { get; set; } = null;
        public string SeoTitle { get; set; } = null;
        public string SeoAlias { get; set; } = null;
        public int LanguageId { get; set; } = 0;
        public Language Language { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}

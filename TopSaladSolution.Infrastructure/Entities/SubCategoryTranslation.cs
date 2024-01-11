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
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}

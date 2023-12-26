using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class ProductTranslation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoAlias { get; set; }
        public int LanguageId { get; set; }


    }
}

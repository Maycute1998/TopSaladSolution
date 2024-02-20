using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Model.Products
{
    public class ProductVM
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int Views { set; get; }
        public DateTime DateCreated { set; get; }

        public List<ProductTranslationVM> ProductTranslations { get; set; }

        public bool? IsFeatured { get; set; }

        //public string ThumbnailImage { get; set; }

        //public List<string> Categories { get; set; } = new List<string>();
    }

    public class ProductTranslationVM
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public string ThumbnailImage { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { set; get; }
        public string? Name { set; get; }
        public string? SubCategoryName { set; get; }
        public string? CategoryName { set; get; }
        public string? Description { set; get; }
        public string? Details { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int Views { set; get; }
        public DateTime ModifiedDate { set; get; }
        public bool? IsFeatured { get; set; }
        public string? ThumbnailImage { get; set; }
        public ItemStatus Status { set; get; }
    }
}

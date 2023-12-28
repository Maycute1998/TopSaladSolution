using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSaladSolution.Model.Products
{
    public class ProductCreateRequest
    {
        
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public int SubCategoryId { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        public bool? IsFeatured { get; set; }
        //public IFormFile ThumbnailImage { get; set; }
    }
}
    
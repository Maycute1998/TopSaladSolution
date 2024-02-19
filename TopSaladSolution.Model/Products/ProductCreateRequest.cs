using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Common.Enums;

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
        public int Views { get; set; }

        public bool? IsFeatured { get; set; }
        //public IFormFile ThumbnailImage { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
    
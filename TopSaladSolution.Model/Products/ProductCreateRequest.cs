using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSaladSolution.Model.Products
{
    public class ProductCreateRequest
    {
        public int SubCategoryId { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
    }
}

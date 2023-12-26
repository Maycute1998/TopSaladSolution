using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int Views { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<OrderDetail> OrderDetails { set; get; }


    }
}

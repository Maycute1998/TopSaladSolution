using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}

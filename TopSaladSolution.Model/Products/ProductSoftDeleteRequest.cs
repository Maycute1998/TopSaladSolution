using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Enums;

namespace TopSaladSolution.Model.Products
{
    public class ProductSoftDeleteRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}

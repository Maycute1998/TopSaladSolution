using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Model.PagingRequest;

namespace TopSaladSolution.Model.Products
{
    public class ProductPagingRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }

        public string? LanguageId { get; set; }

        public int? CategoryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Application.Catalog.Products.DTOs;

namespace TopSaladSolution.Application.Catalog.Products
{
    public interface IProductService
    {
        int Create(ProductCreateRequest request);
        int Update(ProductEditRequest request);
        int Delete(int productId);
        List<ProductVM> GetAll(); 
        List<ProductVM> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}

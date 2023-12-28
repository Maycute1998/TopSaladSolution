using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Interface
{
    public interface IProductService
    {
        Task<object> Create(ProductCreateRequest request);
        Task Update(ProductEditRequest request);
        Task Delete(int productId);
        Task<List<ProductVM>> GetAllAsync();
        Task<ProductVM> GetById(int id);
        Task<List<ProductVM>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}

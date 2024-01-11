using System.Collections.Generic;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetPopularDevelopers(int count);
        Task<List<ProductVM>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}

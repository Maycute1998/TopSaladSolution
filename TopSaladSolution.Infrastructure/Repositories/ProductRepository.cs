using System;
using System.Collections.Generic;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;
using System.Threading.Tasks;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TopSaladDbContext context) : base(context)
        {
        }

        public Task<List<ProductVM>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPopularDevelopers(int count)
        {
            throw new NotImplementedException();
        }
    }
}

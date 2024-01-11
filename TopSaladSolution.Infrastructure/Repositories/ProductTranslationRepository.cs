using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public class ProductTranslationRepository : Repository<ProductTranslation>, IProductTranslationRepository
    {
        private readonly TopSaladDbContext _context;
        public ProductTranslationRepository(TopSaladDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<ProductTranslation> GetByProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentNullException("ProductId is not found");
            }

            ProductTranslation productTranslation = _context.ProductTranslations.FirstOrDefault(x => x.ProductId == productId);
            if (productTranslation is null)
            {
                throw new ArgumentNullException("ProductId is not found");
            }
            return Task.FromResult(productTranslation);
        }
    }
}

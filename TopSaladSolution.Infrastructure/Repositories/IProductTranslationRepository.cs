using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public interface IProductTranslationRepository : IRepository<ProductTranslation>
    {
        Task<ProductTranslation> GetByProductId(int id);
    }
}

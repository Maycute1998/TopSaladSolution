using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        //IRepository<ProductTranslation> ProductTranslationRepository { get; }
        IProductTranslationRepository ProductTranslationRepository { get; }

        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private TopSaladDbContext _context;
        private IRepository<Product> productRepository;
        private IProductTranslationRepository productTranslationRepository;


        public UnitOfWork(TopSaladDbContext context)
        {
            _context = context;
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_context);
                }

                return productRepository;
            }
        }

        public IProductTranslationRepository ProductTranslationRepository
        {
            get
            {
                if (productTranslationRepository == null)
                {
                    productTranslationRepository = new ProductTranslationRepository(_context);
                }

                return productTranslationRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

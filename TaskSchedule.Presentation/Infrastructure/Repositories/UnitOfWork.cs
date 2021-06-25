using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Business.Interfaces;
using TaskSchedule.Presentation.Business.Services;
using TaskSchedule.Presentation.Domain.Intercaces;

namespace TaskSchedule.Presentation.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Business.Interfaces;
using TaskSchedule.Presentation.Domain.Entities;
using TaskSchedule.Presentation.Infrastructure;
using TaskSchedule.Presentation.Infrastructure.Repositories;

namespace TaskSchedule.Presentation.Business.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ApplicationDbContext _context
        {
            get { return Context as ApplicationDbContext; }
        }
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAllProduct()
        {
            throw new NotImplementedException();
        }
    }
}

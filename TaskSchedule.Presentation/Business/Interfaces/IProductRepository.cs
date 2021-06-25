using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Domain.Entities;
using TaskSchedule.Presentation.Domain.Intercaces;

namespace TaskSchedule.Presentation.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllProduct();
    }
}

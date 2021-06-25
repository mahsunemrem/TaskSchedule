using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Business.Interfaces;

namespace TaskSchedule.Presentation.Domain.Intercaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
    }
}

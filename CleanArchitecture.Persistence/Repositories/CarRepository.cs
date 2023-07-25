using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Repositories
{
    public sealed class CarRepository : Repository<Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

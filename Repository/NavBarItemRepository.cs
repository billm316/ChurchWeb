using ChurchWebEntities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class NavBarItemRepository : Repository<NavBarItem>, INavBarItemRepository
    {
        private DbContext _dbContext;

        public NavBarItemRepository(ChurchWebDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

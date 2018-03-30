using ChurchWebEntities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CarouselItemRepository : Repository<CarouselItem>, ICarouselItemRepository
    {
        private DbContext _dbContext;

        public CarouselItemRepository(ChurchWebDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }        
    }
}

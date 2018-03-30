using ChurchWebEntities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public interface IChurchWebDbContext
    {
        DbSet<CarouselItem> CarouselItems { get; }
        DbSet<NavBarItem> NavBarItems { get; }
    }
}

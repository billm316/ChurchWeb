using ChurchWebEntities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ChurchWebDbContext : DbContext 
    {
        public ChurchWebDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<NavBarItem> NavBarItems { get; set; }
    }
}

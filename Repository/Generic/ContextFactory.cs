using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ChurchWebDbContextFactory
    {
        private readonly DbContextOptionsBuilder<ChurchWebDbContext> _options;

        public ChurchWebDbContextFactory(DbContextOptionsBuilder<ChurchWebDbContext> options)
        {
            _options = options;
        }

        public ChurchWebDbContext GetNewDbContext()
        {
            return new ChurchWebDbContext(_options.Options);
        }
    }
}

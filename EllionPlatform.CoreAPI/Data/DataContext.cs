using Microsoft.EntityFrameworkCore;

namespace EllionPlatform.CoreAPI.Data
{
    class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}

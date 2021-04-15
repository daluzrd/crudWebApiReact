using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}

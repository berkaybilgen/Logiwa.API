using Logiwa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logiwa.Data
{
    public class LogiwaDbContext : DbContext
    {
        public LogiwaDbContext(DbContextOptions<LogiwaDbContext> options) : base (options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using RNDR.DAL.Models;

namespace RNDR.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}

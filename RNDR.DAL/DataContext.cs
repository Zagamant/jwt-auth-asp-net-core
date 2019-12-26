using Microsoft.EntityFrameworkCore;
using RNDR.DAL.Models;

namespace RNDR.DAL
{
    /// <summary>
    /// Represents an application database context.
    /// </summary>
    public sealed class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Gets or sets a <see cref="DbSet"/> for <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}

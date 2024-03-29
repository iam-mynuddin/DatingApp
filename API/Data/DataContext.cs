using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<AppUser>().HasData(
                 new AppUser { Id = 1, UserName = "Alice" },
    new AppUser { Id = 2, UserName = "Bob" },
    new AppUser { Id = 3, UserName = "Charlie" },
    new AppUser { Id = 4, UserName = "David" },
    new AppUser { Id = 5, UserName = "Eva" }
            );
        }
    }
}

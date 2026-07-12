using Microsoft.EntityFrameworkCore;
using ClothingStore.Models;

namespace ClothingStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables (DbSets)
        public DbSet<Category> Categories { get; set; }
    }
}
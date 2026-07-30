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
         public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Gender> Genders { get; set; }

    }
}
using Asp_Lesson1.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp_Lesson1.Repositories
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Store> Stores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseMySql("server=localhost;user=root;password=password;database=ProductsAspNet;", new MySqlServerVersion(new Version(8, 0, 36)));
        }
    }
}

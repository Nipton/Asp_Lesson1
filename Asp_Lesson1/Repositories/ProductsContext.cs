using Asp_Lesson1.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp_Lesson1.Repositories
{
    public class ProductsContext : DbContext
    {
        //"server=localhost;user=root;password=password;database=ProductsAspNet;", new MySqlServerVersion(new Version(8, 0, 36))
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Store> Stores { get; set; }
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }
    }
}

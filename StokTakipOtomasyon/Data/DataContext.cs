using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Config;
using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
        }
    }
}

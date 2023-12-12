using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Config
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var products = new List<Product>()
            {
                new Product {
                    Id = 1,
                    Name = "PLC Machine",
                    Description = "Siemens New Generation PLC Machine",
                    Price = 100,
                    StockAmount = 10,
                    WareHouse = new WareHouse { Id = 1, Name = "Warehouse A" }
                },
                new Product {
                    Id = 2,
                    Name = "Raspberry Pi 4",
                    Description = "Microcontroller",
                    Price = 50,
                    StockAmount = 5,
                    WareHouse = new WareHouse { Id = 2, Name = "Warehouse B" }
                }
            };

            builder.HasData(products);
        }
    }
}

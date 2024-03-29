﻿using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}

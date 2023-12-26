using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StokTakipOtomasyon.Data
{
    public class DataAuthContext : IdentityDbContext
    {
        public DataAuthContext(DbContextOptions<DataAuthContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var writerRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";
            var warehouseManagerId = "b21972e1-742f-4fa7-be46-1189d9cab7ca";
            var companyManagerId = "a91a40d3-af24-4e4d-89f3-8794a720e182";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Wrıter".ToUpper()
                },
                new IdentityRole
                {
                    Id = warehouseManagerId,
                    ConcurrencyStamp = warehouseManagerId,
                    Name = "WarehouseManager",
                    NormalizedName = "WarehouseManager".ToUpper()
                },
                new IdentityRole
                {
                    Id = companyManagerId,
                    ConcurrencyStamp = companyManagerId,
                    Name = "CompanyManager",
                    NormalizedName = "CompanyManager".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}

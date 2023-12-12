using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Repositories.Concretes
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly DataContext _dbContext;
        public WareHouseRepository(DataContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<WareHouse?> CreateAsync(WareHouse wareHouse)
        {
            var company = await _dbContext.Companies
                .Include(c => c.WareHouses)
                .FirstOrDefaultAsync(c => c.Id == wareHouse.Company.Id);
            company.WareHouses.Add(wareHouse);
            await _dbContext.WareHouses.AddAsync(wareHouse);
            await _dbContext.SaveChangesAsync();
            return wareHouse;
        }

        public async Task<WareHouse?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WareHouse>> GetAllAsync()
        {
            return await _dbContext.WareHouses
                .Include("Products")
                .Include("Company")
                .ToListAsync();
        }

        public async Task<WareHouse?> GetByIdAsync(int id)
        {
            return await _dbContext.WareHouses
                .Include("Products")
                .Include("Company")
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WareHouse?> UpdateAsync(int id, WareHouse wareHouse)
        {
            var existingWarehouse = await _dbContext.WareHouses
                                            .Include("Products")
                                            .Include("Company")
                                            .FirstOrDefaultAsync(w => w.Id == id);

            if (existingWarehouse == null)
            {
                return null;
            }

            existingWarehouse.Name = wareHouse.Name;
            existingWarehouse.Region = wareHouse.Region;
            existingWarehouse.Country = wareHouse.Country;
            existingWarehouse.City = wareHouse.City;
            existingWarehouse.District = wareHouse.District;

            await _dbContext.SaveChangesAsync();
            return existingWarehouse;

        }
    }
}

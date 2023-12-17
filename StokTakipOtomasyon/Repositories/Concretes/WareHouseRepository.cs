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

        public async Task<WareHouse?> CreateAsync(WareHouse wareHouse, int companyId)
        {
            // Check if company with given Id exists
            var company = await _dbContext.Companies
                            .Include(c => c.WareHouses)
                            .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company is null)
            {
                return null;
            }

            // assign company to warehouse
            wareHouse.Company = company;

            // Add new created warehouse into database
            await _dbContext.WareHouses.AddAsync(wareHouse);
            await _dbContext.SaveChangesAsync();
            return wareHouse;
        }

        public async Task<WareHouse?> DeleteAsync(int id)
        {
            var wareHouse = await _dbContext.WareHouses
                                .Include(wareHouse => wareHouse.Company)
                                .Include(wareHouse => wareHouse.Products)
                                .FirstOrDefaultAsync(w => w.Id == id);

            if (wareHouse == null)
            {
                return null;
            }

            _dbContext.WareHouses.Remove(wareHouse);
            await _dbContext.SaveChangesAsync();
            return wareHouse;
        }

        public async Task<List<WareHouse>> GetAllAsync(string? filterOn, string? filterQuery)
        {
            // Get IQueryable for Filtering
            var wareHouses = _dbContext.WareHouses
                             .Include(w => w.Products)
                             .Include(w => w.Company)
                             .AsQueryable();

            // Filtering
            if (String.IsNullOrWhiteSpace(filterOn) == false && String.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    wareHouses = wareHouses.Where(w => w.Name.Contains(filterQuery));
                }
                if (filterOn.Equals("Region", StringComparison.OrdinalIgnoreCase))
                {
                    wareHouses = wareHouses.Where(w => w.Region.Equals(filterQuery));
                }
            }

            return await wareHouses.ToListAsync();
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

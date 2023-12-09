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
        public async Task<WareHouse?> AddWareHouse(WareHouse wareHouse)
        {
            var company = await _dbContext.Companies
                .Include(c => c.WareHouses)
                .FirstOrDefaultAsync(c => c.Id == wareHouse.Company.Id);
            company.WareHouses.Add(wareHouse);
            await _dbContext.WareHouses.AddAsync(wareHouse);
            await _dbContext.SaveChangesAsync();
            return wareHouse;
        }

        public async Task<WareHouse?> DeleteWareHouse(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WareHouse>> GetAllWareHousesAsync()
        {
            return await _dbContext.WareHouses
                .Include("Products")
                .Include("Company")
                .ToListAsync();
        }

        public async Task<WareHouse?> GetWareHouseByIdAsync(int id)
        {
            return await _dbContext.WareHouses
                .Include("Products")
                .Include("Company")
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WareHouse?> UpdateWareHouseAsync(int id, WareHouse wareHouse)
        {
            throw new NotImplementedException();
        }
    }
}

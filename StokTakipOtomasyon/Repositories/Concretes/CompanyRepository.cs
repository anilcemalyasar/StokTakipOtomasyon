using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Repositories.Concretes
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dbContext;
        public CompanyRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Company?> AddCompanyAsync(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task<Company?> AssignWareHouseToCompany(Company company, WareHouse wareHouse)
        {
            // Assign company to warehouse
            wareHouse.Company = company;

            // Assign warehouse to company
            company.WareHouses.Add(wareHouse);

            await _dbContext.SaveChangesAsync();
            return company;

        }

        public Task<Company?> DeleteCompanyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dbContext.Companies
                .Include(company => company.WareHouses)
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _dbContext.Companies
                .Include(company => company.WareHouses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Company?> UpdateCompanyAsync(int id, Company company)
        {
            throw new NotImplementedException();
        }
    }
}

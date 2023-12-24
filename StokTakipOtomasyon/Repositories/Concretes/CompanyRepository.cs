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

        public async Task<Company?> DeleteCompanyByIdAsync(int id)
        {
            var companyDomainModel = await _dbContext.Companies
                                            .Include(company => company.WareHouses)
                                            .FirstOrDefaultAsync(c => c.Id == id);

            if (companyDomainModel is null)
            {
                return null;
            }

            _dbContext.Companies.Remove(companyDomainModel);
            await _dbContext.SaveChangesAsync();
            return companyDomainModel;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dbContext.Companies
                .Include(company => company.WareHouses)
                .ThenInclude(warehouse => warehouse.Products)
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _dbContext.Companies
                .Include(company => company.WareHouses)
                .ThenInclude(warehouse => warehouse.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company?> UpdateCompanyAsync(int id, Company company)
        {
            var companyDomainModel = await _dbContext.Companies
                                            .Include(company => company.WareHouses)
                                            .FirstOrDefaultAsync(c => c.Id == id);

            // Check if exists
            if (companyDomainModel is null)
            {
                return null;
            }

            // Update properties
            companyDomainModel.Name = company.Name;
            companyDomainModel.Country = company.Country;
            companyDomainModel.City = company.City;
            companyDomainModel.CompanyCode = company.CompanyCode;

            await _dbContext.SaveChangesAsync();
            return companyDomainModel;

        }

        public async Task<Company?> UpdateCompanyNameAsync(int id, string? companyName)
        {
            // Find existing company
            var company = await _dbContext.Companies
                            .Include(company => company.WareHouses)
                            .FirstOrDefaultAsync(c => c.Id == id);

            // Check if exists
            if (company is null)
            {
                return null;
            }

            // Check if new company name blank
            if (!String.IsNullOrWhiteSpace(companyName))
            {
                // Update Company Name property
                company.Name = companyName;
            }

            // Save changes
            await _dbContext.SaveChangesAsync();
            return company;

        }
    }
}

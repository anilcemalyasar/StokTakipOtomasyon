using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> AddCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(int id, Company company);
        Task<Company?> DeleteCompanyByIdAsync(int id);
        Task<Company?> AssignWareHouseToCompany(Company company, WareHouse wareHouse);

        Task<Company?> UpdateCompanyNameAsync(int id, string? companyName);
    }
}

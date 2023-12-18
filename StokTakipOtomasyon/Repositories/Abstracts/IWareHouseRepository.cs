using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface IWareHouseRepository
    {
        Task<List<WareHouse>> GetAllAsync(string? filterOn=null, string? filterQuery = null
                ,string? sortBy=null, bool isAscending = true, int pageNumber=1, int pageSize=3);
        Task<WareHouse?> GetByIdAsync(int id);
        Task<WareHouse?> CreateAsync(WareHouse wareHouse, int companyId);
        Task<WareHouse?> UpdateAsync(int id, WareHouse wareHouse);
        Task<WareHouse?> DeleteAsync(int id);

    }
}

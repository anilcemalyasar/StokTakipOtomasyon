using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface IWareHouseRepository
    {
        Task<List<WareHouse>> GetAllAsync();
        Task<WareHouse?> GetByIdAsync(int id);
        Task<WareHouse?> CreateAsync(WareHouse wareHouse);
        Task<WareHouse?> UpdateAsync(int id, WareHouse wareHouse);
        Task<WareHouse?> DeleteAsync(int id);

    }
}

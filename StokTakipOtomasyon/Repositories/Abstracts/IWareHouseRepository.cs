using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface IWareHouseRepository
    {
        Task<List<WareHouse>> GetAllWareHousesAsync();
        Task<WareHouse?> GetWareHouseByIdAsync(int id);
        Task<WareHouse?> AddWareHouse(WareHouse wareHouse);
        Task<WareHouse?> UpdateWareHouseAsync(int id, WareHouse wareHouse);
        Task<WareHouse?> DeleteWareHouse(int id);

    }
}
